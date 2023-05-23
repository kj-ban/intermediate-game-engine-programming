using System;
using UnityEngine;

namespace Gpm.CacheStorage
{
    [Serializable]
    public struct StringToValue<T>
    {
        [SerializeField]
        private string text;

        private T value;

        private bool converted;

        public string GetText()
        {
            if (converted == true)
            {
                if (string.IsNullOrEmpty(text) == true)
                {
                    text = value.ToString();
                }
            }
            return text;
        }

        public T GetValue()
        {
            if (converted == false)
            {
                return ConvertValue();
            }

            return value;
        }

        private T ConvertValue()
        {
            try
            {
                if (string.IsNullOrEmpty(text) == false)
                {
                    value = (T)Convert.ChangeType(text, typeof(T));
                    converted = true;
                }
            }
            catch
            {
                SetText(string.Empty);
            }

            return value;
        }

        public StringToValue(string text = "")
        {
            if(string.IsNullOrEmpty(text) == true)
            {
                text = string.Empty;
            }

            this.text = text;

            this.value = default(T);

            this.converted = false;
        }

        public StringToValue(T value)
        {
            if (value == null)
            {
                value = default(T);
            }

            this.text = string.Empty;

            this.value = value;

            this.converted = true;
        }

        public void SetText(string text)
        {
            if (string.IsNullOrEmpty(text) == true)
            {
                text = string.Empty;
            }

            if (text.Equals(this.text) == false)
            {
                this.text = text;

                this.value = default(T);

                this.converted = false;
            }
        }

        public void SetValue(T value)
        {
            if (value == null)
            {
                value = default(T);
            }
            this.value = value;

            this.converted = true;
        }

        public bool IsValid()
        {
            if (converted == false)
            {
                if (string.IsNullOrEmpty(text) == false)
                {
                    ConvertValue();
                }
            }

            return converted;
        }

        public static implicit operator T(StringToValue<T> data)
        {
            return data.GetValue();
        }

        public static implicit operator string(StringToValue<T> data)
        {
            return data.GetText();
        }

        public static implicit operator StringToValue<T>(string value)
        {
            return new StringToValue<T>(value);
        }

        public static implicit operator StringToValue<T>(T value)
        {
            return new StringToValue<T>(value);
        }

        public override string ToString()
        {
            return GetText();
        }
    }
}