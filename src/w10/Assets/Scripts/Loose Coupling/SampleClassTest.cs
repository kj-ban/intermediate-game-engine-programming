using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControl
{
    void Paint();
}

public interface ISurface
{
    void Paint();
}

public class SampleClass : IControl, ISurface
{
    public void Paint()
    {
        Debug.Log("PaintMethod in SampleClass");
    }
}

public class SampleClass2nd : IControl, ISurface
{
    void IControl.Paint()
    {
        Debug.Log("IControl.Paint");
    }

    void ISurface.Paint()
    {
        Debug.Log("ISurface.Paint");
    }
}

public class SampleClassTest : MonoBehaviour
{
    public void Start()
    {
        SampleClass sample = new SampleClass();
        IControl control = sample;
        ISurface surface = sample;

        sample.Paint();
        control.Paint();
        surface.Paint();

        SampleClass2nd sample2 = new SampleClass2nd();
        IControl control2 = sample2;
        ISurface surface2 = sample2;

        //sample2.Paint();
        control2.Paint();
        surface2.Paint();
    }
}