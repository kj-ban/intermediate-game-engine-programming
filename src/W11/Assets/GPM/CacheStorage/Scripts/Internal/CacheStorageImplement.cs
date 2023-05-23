namespace Gpm.CacheStorage.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Resolvers;
    using Common;
    using Common.Log;
    using Common.Util;

    internal static class CacheStorageImplement
    {
        internal const string NAME = "CacheStorageImplement";

        internal static event Action onChangeCache;

        internal static CacheStorageConfig cacheConfig = new CacheStorageConfig();
        internal static CachePackage cachePackage;

        internal static long updateTime = 0;

        internal static bool Initialized = false;

        public static void Initialize(int maxCount, int maxSize, double reRequestTime, CacheRequestType defaultRequestType, double unusedPeriodTime, double removeCycle)
        {
            if (cacheConfig.setting == false)
            {
                cacheConfig = new CacheStorageConfig(maxCount, maxSize, reRequestTime, unusedPeriodTime, removeCycle, defaultRequestType);
            }

            InitializeCore();
        }

        internal static void InitializeCore()
        {
            if (Initialized == false)
            {
                GpmMessagePackMapper.Initialize(CacheStorageResolver.Instance);

                cachePackage = CachePackage.Load();
                if (cachePackage == null)
                {
                    cachePackage = new CachePackage();
                }

                updateTime = DateTime.UtcNow.Ticks;

                UnityEngine.Application.focusChanged += OnFocusChanged;
                UnityEngine.Application.quitting += OnQuit;

                ManagedCoroutine.Start(UpdateRoutine());

                Initialized = true;
            }
        }

        internal static bool IsSetting()
        {
            return cacheConfig.setting;
        }

        internal static int GetCacheCount()
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return 0;
            }

            return cachePackage.cacheStorage.Count;
        }

        internal static int GetMaxCount()
        {
            return cacheConfig.maxCount;
        }

        internal static long GetCacheSize()
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return 0;
            }

            return cachePackage.cachedSize;
        }

        internal static long GetMaxSize()
        {
            return cacheConfig.maxSize;
        }

        internal static double GetReRequestTime()
        {
            return cacheConfig.reRequestTime;
        }

        internal static CacheRequestType GetCacheRequestType()
        {
            return cacheConfig.defaultRequestType;
        }

        internal static double GetUnusedPeriodTime()
        {
            return cacheConfig.unusedPeriodTime;
        }

        internal static double GetRemoveCycle()
        {
            return cacheConfig.removeCycle;
        }
        internal static long GetRemoveCacheSize()
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return 0;
            }
            return cachePackage.removeCacheSize;
        }

        internal static CacheInfo Request(string url, CacheRequestType requestType, double reRequestTime, Action<GpmCacheResult> onResult)
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return null;
            }

            return cachePackage.Request(url, requestType, reRequestTime, onResult);
        }

        internal static CacheInfo RequestHttpCache(string url, Action<GpmCacheResult> onResult)
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return null;
            }

            return Request(url, CacheRequestType.ALWAYS, 0, onResult);
        }

        internal static CacheInfo RequestLocalCache(string url, Action<GpmCacheResult> onResult)
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return null;
            }

            return cachePackage.RequestLocal(url, onResult);
        }

        internal static CacheInfo GetCachedTexture(string url, Action<CachedTexture> onResult)
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return null;
            }

            CacheInfo info = cachePackage.GetCacheInfo(url);
            if (info != null)
            {
                CachedTexture cachedTexture = CachedTextureManager.Get(info);
                if (cachedTexture != null)
                {
                    onResult?.SafeCallback(cachedTexture);
                    return info;
                }
            }

            return RequestLocalCache(url, (result) =>
            {
                if (result.IsSuccess() == true)
                {
                    onResult?.SafeCallback(CachedTextureManager.Cache(result.Info, false, false, result.Data));
                }
                else
                {
                    onResult?.SafeCallback(null);
                }
            });
        }

        internal static CacheInfo RequestTexture(string url, CacheRequestType requestType, double reRequestTime, bool preLoad, Action<CachedTexture> onResult)
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return null;
            }

            CacheInfo info = cachePackage.GetCacheInfo(url);
            if (info != null)
            {
                bool loaded = false;
                bool needRequest = info.NeedRequest(reRequestTime);
                if (needRequest == false ||
                    preLoad == true)
                {
                    CachedTexture cachedTexture = CachedTextureManager.Get(info);
                    if (cachedTexture != null)
                    {
                        onResult?.SafeCallback(cachedTexture);

                        info.lastAccess = DateTime.UtcNow.Ticks;
                        SetDirty();

                        loaded = true;
                    }
                    else
                    {
                        if (cachePackage.IsValidCacheData(info) == true)
                        {
                            byte[] datas = cachePackage.GetCacheData(info);

                            onResult?.SafeCallback(CachedTextureManager.Cache(info, false, false, datas));

                            info.lastAccess = DateTime.UtcNow.Ticks;
                            SetDirty();

                            loaded = true;
                        }
                        else
                        {
                            info.eTag = string.Empty;
                            info.lastModified = string.Empty;
                            requestType = CacheRequestType.ALWAYS;
                        }
                    }
                }

                if(loaded == true &&
                   needRequest == false)
                {
                    return info;
                }
            }

            bool subRequest = false;
            if (cachePackage.GetRequestCache(url) != null)
            {
                subRequest = true;
            }

            info = Request(url, requestType, 0, (result) =>
            {
                if (result.IsSuccess() == true)
                {
                    CachedTexture resultTexture = null;
                    if (subRequest == true)
                    {
                        resultTexture = CachedTextureManager.Get(info);
                    }

                    if (resultTexture == null)
                    {
                        resultTexture = CachedTextureManager.Cache(result.Info, true, result.UpdateData, result.Data);
                    }

                    onResult?.SafeCallback(resultTexture);
                }
                else
                {
                    onResult?.SafeCallback(null);
                }
            });

            return info;
        }

        internal static void ClearCache()
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return;
            }

            cachePackage.RemoveAll();
        }

        private static IEnumerator UpdateRoutine()
        {
            while (cachePackage != null)
            {
                updateTime = DateTime.UtcNow.Ticks;

                cachePackage.Update();

                AutoDeleteUnusedCache();

                yield return null;
            }
        }

        private static void AutoDeleteUnusedCache()
        {
            if (GetUnusedPeriodTime() > 0 &&
                GetRemoveCycle() > 0)
            {
                cachePackage.SecuringStorageLastAccess(GetUnusedPeriodTime());
            }
        }

        internal static void AddChangeCacheEvnet(Action callback)
        {
            onChangeCache += callback;
        }

        internal static void ClearChangeCacheEvent()
        {
            onChangeCache = null;
        }

        internal static bool IsDirty()
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return false;
            }

            return cachePackage.IsDirty();
        }


        internal static void SetDirty(bool value = true)
        {
            if (Initialized == false)
            {
                GpmLogger.Error("Not initialized", NAME, typeof(CacheStorageImplement));
                return;
            }

            cachePackage.SetDirty(value);

            try
            {
                onChangeCache?.Invoke();
            }
            catch(Exception e)
            {
                GpmLogger.Exception(e);
            }
        }


        internal static void SavePackage()
        {
            cachePackage.Save();

            try
            {
                onChangeCache?.Invoke();
            }
            catch (Exception e)
            {
                GpmLogger.Exception(e);
            }
        }


        internal static void OnFocusChanged(bool focus)
        {
            if (focus == false)
            {
                if (cachePackage.IsDirty() == true)
                {
                    SavePackage();
                }
            }
        }
        internal static void OnQuit()
        {
            if (cachePackage.IsDirty() == true)
            {
                SavePackage();
            }
        }

    }
}