using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class MyExtensions
{
    #region RectTransform扩展
    public enum AnchorPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottonCenter,
        BottomRight,
        BottomStretch,

        VertStretchLeft,
        VertStretchRight,
        VertStretchCenter,

        HorStretchTop,
        HorStretchMiddle,
        HorStretchBottom,

        StretchAll
    }
    public enum PivotPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight,
    }

    public static void SetAnchor(this RectTransform source, AnchorPresets allign, float offsetX = 0, float offsetY = 0)
    {
        source.anchoredPosition = new Vector3(offsetX, offsetY, 0);

        switch (allign)
        {
            case (AnchorPresets.TopLeft):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.TopCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 1);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.TopRight):
                {
                    source.anchorMin = new Vector2(1, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }

            case (AnchorPresets.MiddleLeft):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(0, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0.5f);
                    source.anchorMax = new Vector2(0.5f, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleRight):
                {
                    source.anchorMin = new Vector2(1, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }

            case (AnchorPresets.BottomLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 0);
                    break;
                }
            case (AnchorPresets.BottonCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f, 0);
                    break;
                }
            case (AnchorPresets.BottomRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }

            case (AnchorPresets.HorStretchTop):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
            case (AnchorPresets.HorStretchMiddle):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }
            case (AnchorPresets.HorStretchBottom):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }

            case (AnchorPresets.VertStretchLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.VertStretchCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.VertStretchRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }

            case (AnchorPresets.StretchAll):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
        }
    }

    public static void SetPivot(this RectTransform source, PivotPresets preset)
    {

        switch (preset)
        {
            case (PivotPresets.TopLeft):
                {
                    source.pivot = new Vector2(0, 1);
                    break;
                }
            case (PivotPresets.TopCenter):
                {
                    source.pivot = new Vector2(0.5f, 1);
                    break;
                }
            case (PivotPresets.TopRight):
                {
                    source.pivot = new Vector2(1, 1);
                    break;
                }

            case (PivotPresets.MiddleLeft):
                {
                    source.pivot = new Vector2(0, 0.5f);
                    break;
                }
            case (PivotPresets.MiddleCenter):
                {
                    source.pivot = new Vector2(0.5f, 0.5f);
                    break;
                }
            case (PivotPresets.MiddleRight):
                {
                    source.pivot = new Vector2(1, 0.5f);
                    break;
                }

            case (PivotPresets.BottomLeft):
                {
                    source.pivot = new Vector2(0, 0);
                    break;
                }
            case (PivotPresets.BottomCenter):
                {
                    source.pivot = new Vector2(0.5f, 0);
                    break;
                }
            case (PivotPresets.BottomRight):
                {
                    source.pivot = new Vector2(1, 0);
                    break;
                }
        }
    }
    #endregion

    #region EventTrigger

    /// <summary>
    /// Transform扩展添加 EventTrigger 事件
    /// </summary>
    /// <param name="trs"></param>
    /// <param name="eventID">事件类型</param>
    /// <param name="callback">事件回调</param>
    public static void AddTrsEntryTriggerEvent(this Transform trs, EventTriggerType eventID, UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = null;
        trigger = trs.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = trs.gameObject.AddComponent<EventTrigger>();
        }
        var entry = trigger.triggers.Find((_entry) => { return _entry.eventID == eventID; });
        if (entry == null)
        {
            entry = new EventTrigger.Entry();
            entry.eventID = eventID;
            trigger.triggers.Add(entry);
        }
        entry.callback.AddListener(callback);
    }
    /// <summary>
    /// 检查鼠标是否在UI上
    /// </summary>
    /// <param name="uiPanel"></param>
    /// <returns></returns>
    public static bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, result);
        if (result.Count > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 检查鼠标是否在目标UI上
    /// </summary>
    /// <param name="uiPanel"></param>
    /// <returns></returns>
    public static bool IsPointerOverUI(this Transform uiPanel)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, result);
            if (result.Count > 0)
                foreach (var item in result)
                {
                    if (item.gameObject == uiPanel.gameObject || item.gameObject.transform.IsChildOf(uiPanel)) return true;
                }
        }
        return false;
    }

    /// <summary>
    /// 多个个 检查值
    /// </summary>
    /// <param name="uiPanel"></param>
    /// <returns></returns>
    public static bool IsPointerOverUI(List<Transform> uiPanels)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, result);
            if (result.Count > 0)
                foreach (var item in result)
                {
                    if (uiPanels != null) return false;
                    foreach (var u in uiPanels)
                    {
                        if (item.gameObject == u.gameObject || item.gameObject.transform.IsChildOf(u)) return true;
                    }
                }
        }
        return false;
    }

    /// <summary>
    ///  返回单个个UI检查值
    /// </summary>
    /// <param name="uiPanel"></param>
    /// <param name="mouseStatus"></param>
    /// <returns></returns>
    public static bool IsPointerOverUI(this Transform uiPanel, MouseStatus mouseStatus = MouseStatus.DownAndWheel)
    {
        switch (mouseStatus)
        {
            case MouseStatus.DownAndWheel:
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    return IsPointerOverUI(uiPanel);
                }
                return false;
            case MouseStatus.Down:
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    return IsPointerOverUI(uiPanel);
                }
                return false;
            case MouseStatus.Up:
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                {
                    return IsPointerOverUI(uiPanel);
                }
                return false;
            case MouseStatus.UpAndWheel:
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    return IsPointerOverUI(uiPanel);
                }
                return false;
        }
        return false;
    }
    /// <summary>
    /// 返回多个UI检查值
    /// </summary>
    /// <param name="uiPanels"></param>
    /// <param name="mouseStatus"></param>
    /// <returns></returns>
    public static bool IsPointerOverUI(this List<Transform> uiPanels, MouseStatus mouseStatus = MouseStatus.DownAndWheel)
    {
        switch (mouseStatus)
        {
            case MouseStatus.DownAndWheel:
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    return IsPointerOverUI(uiPanels);
                }
                return false;
            case MouseStatus.Down:
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    return IsPointerOverUI(uiPanels);
                }
                return false;
            case MouseStatus.Up:
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                {
                    return IsPointerOverUI(uiPanels);
                }
                return false;
            case MouseStatus.UpAndWheel:
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    return IsPointerOverUI(uiPanels);
                }
                return false;
        }
        return false;
    }
    /// <summary>
    /// 鼠标可选状态
    /// </summary>
    public enum MouseStatus
    {
        Down = 0,//左右键按下去
        Up = 1,//左右键抬起
        DownAndWheel = 2,//左右键按下去+滑轮
        UpAndWheel = 3,//左右键抬起+滑轮
    }

    #endregion


    #region Transform
    /// <summary>
    /// 获得弧度移动初速度向量（跟RadianMove配合使用）
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="speed">返回初速度向量</param>
    /// <param name="Statpoint">移动的开始点</param>
    /// <param name="Endpoint">移动的结束点</param>
    /// <param name="time">移动的时间</param>
    /// <param name="AccelerationOfGravity">重力加速度</param>
    public static void GetRadianParameter(this Transform transform, ref Vector3 speed, Transform Statpoint, Transform Endpoint, float time, float AccelerationOfGravity)
    {
        transform.position = Statpoint.position;//将物体置于A点
        //通过一个式子计算初速度
        speed = new Vector3((Endpoint.position.x - Statpoint.position.x) / time,
            (Endpoint.position.y - Statpoint.position.y) / time - 0.5f * AccelerationOfGravity * time, (Endpoint.position.z - Statpoint.position.z) / time);
    }

    /// <summary>
    /// 曲线移动
    /// </summary>
    /// <param name="transform">12</param>
    /// <param name="vec3Speed">初速度</param>
    /// <param name="DTime">总时间</param>
    /// <param name="AccelerationOfGravity">重力加速度</param>
    public static void RadianMove(this Transform transform, Vector3 vec3Speed, ref float DTime, float AccelerationOfGravity)
    {
        Vector3 vec3Gravity = new Vector3();
        vec3Gravity.y = AccelerationOfGravity * (DTime += Time.fixedDeltaTime);
        transform.position += vec3Speed * Time.fixedDeltaTime;
        transform.position += vec3Gravity * Time.fixedDeltaTime;
    }

    /// <summary>
    /// 查找子节点对象
    /// 内部使用“递归算法”
    /// </summary>
    /// <param name="goParent">父对象</param>
    /// <param name="chiildName">查找的子对象名称</param>
    /// <returns></returns>
    public static Transform FindTheChildNode(this Transform _transform, GameObject goParent, string chiildName)
    {
        //查找结果

        _transform = goParent.transform.Find(chiildName);
        if (_transform == null)
        {
            foreach (Transform trans in goParent.transform)
            {
                _transform = FindTheChildNode(_transform, trans.gameObject, chiildName);
                if (_transform != null)
                {
                    return _transform;

                }
            }
        }
        return _transform;
    }

    /// <summary>
    /// 获取子节点（对象）脚本
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="goParent">父对象</param>
    /// <param name="childName">子对象名称</param>
    /// <returns></returns>
    public static T GetTheChildNodeComponetScripts<T>(this Transform _transform, GameObject goParent, string childName) where T : Component
    {

        _transform = FindTheChildNode(_transform, goParent, childName);
        if (_transform != null)
        {
            return _transform.gameObject.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }

    ///// <summary>
    ///// 给子节点添加脚本
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="goParent">父对象</param>
    ///// <param name="childName">子对象名称</param>
    ///// <returns></returns>
    //public static T AddChildNodeCompnent<T>(GameObject goParent, string childName) where T : Component
    //{
    //    Transform searchTranform = null;                //查找特定节点结果

    //    //查找特定子节点
    //    searchTranform = FindTheChildNode(goParent, childName);
    //    //如果查找成功，则考虑如果已经有相同的脚本了，则先删除，否则直接添加。
    //    if (searchTranform != null)
    //    {
    //        //如果已经有相同的脚本了，则先删除
    //        T[] componentScriptsArray = searchTranform.GetComponents<T>();
    //        for (int i = 0; i < componentScriptsArray.Length; i++)
    //        {
    //            if (componentScriptsArray[i] != null)
    //            {
    //                Destroy(componentScriptsArray[i]);
    //            }
    //        }
    //        return searchTranform.gameObject.AddComponent<T>();
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //    //如果查找不成功，返回Null.
    //}

    /// <summary>
    /// 给子节点添加父对象
    /// </summary>
    /// <param name="parents">父对象的方位</param>
    /// <param name="child">子对象的方法</param>
    public static void AddChildNodeToParentNode(this Transform parents, Transform child)
    {
        child.SetParent(parents, false);
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
    }











    #endregion


    #region Camera
    /// <summary>
    /// 判断3D物体是否在屏幕内
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="Pos">目标位置</param>
    /// <returns></returns>
    public static bool IsCamereWithin(this Camera camera, Vector3 Pos)
    {
        Vector2 Vec3 = camera.WorldToScreenPoint(Pos);
        if (Vec3.x > Screen.width || Vec3.x < 0 ||
           Vec3.y > Screen.height || Vec3.y < 0)
            return false;

        else return true;
    }
    #endregion


    #region Button
    public static void AddTrsListener(this Transform tra, UnityAction callback)
    {
        Button button;
        if (tra.GetComponent<Button>() != null)
        {
            button = tra.GetComponent<Button>();
        }
        else
        {
            button = tra.gameObject.AddComponent<Button>();
        }
        button.onClick.AddListener(callback);

    }


    #endregion

    #region MeshRenderer

    /// <summary>
    ///     返回物体在3D空间真实的RectTransform.rect.Hight Width
    /// </summary>
    /// <param name="meshRenderer"></param>
    /// <returns></returns>
    public static Vector2 ObjectCtualSize(this MeshRenderer meshRenderer)
    {
        Bounds bounds = meshRenderer.bounds;
        Vector3 Max_X = bounds.center;//依次获取物体bounds  X Y Z的最大和最小值

        Max_X.x += bounds.extents.x;//extents的大小是bounds的一半  所以直接与center加减  取得X Y Z的最大和最小值

        Vector3 Min_X = bounds.center;

        Min_X.x -= bounds.extents.x;



        Vector3 Max_Y = bounds.center;

        Max_X.y += bounds.extents.y;

        Vector3 Min_Y = bounds.center;

        Min_Y.y -= bounds.extents.y;



        Vector3 Max_Z = bounds.center;

        Max_X.z += bounds.extents.z;

        Vector3 Min_Z = bounds.center;

        Max_X.z -= bounds.extents.z;



        //将计算好的6个点转换为屏幕坐标   只需要X   与Y值

        float[] X = new float[] { Camera.main.WorldToScreenPoint(Max_X).x, Camera.main.WorldToScreenPoint(Min_X).x, Camera.main.WorldToScreenPoint(Max_Y).x, Camera.main.WorldToScreenPoint(Min_Y).x, Camera.main.WorldToScreenPoint(Max_Z).x, Camera.main.WorldToScreenPoint(Min_Z).x };



        float[] Y = new float[] { Camera.main.WorldToScreenPoint(Max_X).y, Camera.main.WorldToScreenPoint(Min_X).y, Camera.main.WorldToScreenPoint(Max_Y).y, Camera.main.WorldToScreenPoint(Min_Y).y, Camera.main.WorldToScreenPoint(Max_Z).y, Camera.main.WorldToScreenPoint(Min_Z).y };





        float Up = Mathf.Max(Y);//依次获取  屏幕最高点

        float Down = Mathf.Min(Y);//最低点

        float Left = Mathf.Min(X);//最左侧点

        float Right = Mathf.Max(X);//最右侧点


        float Hight = Up - Down;//物体屏幕渲染高度

        float Width = Right - Left;//物体屏幕渲染宽度

        return (new Vector3(Width, Hight));
    }

    #endregion

    #region     Vecter3

   

    #endregion 

}
