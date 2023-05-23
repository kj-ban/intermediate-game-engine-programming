using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseCouplingSample : MonoBehaviour
{
    public interface IRemote
    {
        void Run();
    }

    public class Television : IRemote
    {
        private static Television _television;

        protected Television()
        {

        }

        static Television()
        {
            _television = new Television();
        }

        public static Television Instance
        {
            get
            {
                return _television;
            }
        }

        public void Run()
        {
            Debug.Log("Television is started!");
        }
    }

    public class Radio : IRemote
    {
        private static Radio _radio;

        protected Radio()
        {

        }

        static Radio()
        {
            _radio = new Radio();
        }

        public static Radio Instance
        {
            get
            {
                return _radio;
            }
        }

        public void Run()
        {
            Debug.Log("Radio is started!");
        }
    }


    public class Remote
    {
        private IRemote _remote;

        public Remote(IRemote remote)
        {
            _remote = remote;
        }

        public void Run()
        {
            _remote.Run();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Remote remoteTV = new Remote(Television.Instance);
        remoteTV.Run();

        Remote remoteRadio = new Remote(Radio.Instance);
        remoteRadio.Run();
    }
}

