//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class temp : MonoBehaviour
//{
//    // Start is called before the first frame update
//    // Add it to an object in your scene, and at Play time it will draw in the Scene View a small yellow line between the scene origin, and a position interpolated between two other positions
//    // ... (one on the up axis, one on the forward axis).
//    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
//    int elapsedFrames = 0;
//    AnimationCurve curve;

//    void Update()
//    {
//        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;//interpolationFramesCount可理解为总共45个工作量，elapsedFrames为经历的工作量
//        //然后这个elapsedFrames要从零开始

//        //Lerp: the value returned equals a + (b - a) * t (which can also be written a * (1-t) + b*t).
//        Vector3 interpolatedPosition = Vector3.Lerp(Vector3.up, Vector3.forward, interpolationRatio);

//        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
//        // 1 % 46 = 1
//        // 2 % 46 = 2
//        // 45 % 46 = 45 这样interpolationRatio = 1到达终点
//        // 46 % 46 = 0
//        // 47 % 46 = 1

//        Debug.DrawLine(Vector3.zero, Vector3.up, Color.green);
//        Debug.DrawLine(Vector3.zero, Vector3.forward, Color.blue);
//        Debug.DrawLine(Vector3.zero, interpolatedPosition, Color.yellow);
//    }

//    void AnotherWayUpdate()
//    {
//        //如果只跑一次不想重置为0可以设置成 
//        elapsedFrames += Time.deltaTime;//根据一帧时间增加
//        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
//        Vector3 interpolatedPosition = Vector3.Lerp(Vector3.up, Vector3.forward, interpolationRatio);//lerp function到达终点就会自己停止，当这个t值>1时不作为

//        //如果写成这样就会变成无限流 无限接近位置 但不会真正到达 而且一开始速度很快，因为Time.deltaTime从0开始计算，到后面无限增加
//        //不推荐这种消耗资源的做法
//        Vector3 interpolatedPosition = Vector3.Lerp(Vector3.up, Vector3.forward, Time.deltaTime);

//        //比较推荐的 跟上面反过来情况比如一开始很慢后面很快这样运动不均速的方法是
//        Vector3 interpolatedPosition = Vector3.Lerp(Vector3.up, Vector3.forward, Mathf.SmoothStep(0,1, interpolationRatio));

//        //在编辑界面会出现一个让你编辑curve的界面，你可以选择然后让物体跟着这个curve运动（好牛啊 以后用）
//        //还可以在curve界面里AddKey来让物体在几个点停一下
//        Vector3 interpolatedPosition = Vector3.Lerp(Vector3.up, Vector3.forward, curve.Evaluate( interpolationRatio));
//    }
//}
