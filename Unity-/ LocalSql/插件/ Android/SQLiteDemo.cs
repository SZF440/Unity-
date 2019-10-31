using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using SUIFW;
using System.Collections.Generic;
public class SQLiteDemo : MonoBehaviour
{

    private string[] AllFields = new string[9] { "SerialNumber", "Name", "Gender", "Age", "Address", "PhoneNumber", "Doctor", "RegistrationTime", "Remarks" };
    public string TalbleName = "PlayerData";
    /// <summary>
    /// SQLite数据库辅助类
    /// </summary>
    private SQLiteHelper sql;
    public static SQLiteDemo thisSQLiteDemo;

    DownEventManager mDownEventManager;
    public DownEventManager MDownEventManager
    {
        get { if (mDownEventManager == null) mDownEventManager = UnityHelper.GetTheChildNodeComponetScripts<DownEventManager>(GameObject.Find("Canvas(Clone)"), "TestManager"); return mDownEventManager; }

    }




    void Start()
    {
        thisSQLiteDemo = this;

        CreateDatabase();
        CreateTable();
         
        //FindField("SerialNumber");
        //FindField("Name");
        //FindField("Gender");
        //FindField("Age");
        //for (int i = 0; i < AllFields.Length; i++)
        //{
        //    FindField(AllFields[i]);
        //}

        //InsertData(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
        ////InsertData(new string[] { "2", "2", "3", "4", "5", "6", "7", "8", "9" });
        ////InsertData(new string[] { "3", "2", "3", "4", "5", "6", "7", "8", "9" });
        //InsertData(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
        //QueryData("1");
        ////更新数据，将Name="张三"的记录中的Name改为"Zhang3"
        //sql.UpdateValues("table1", new string[] { "Name" }, new string[] { "'Zhang3'" }, "Name", "=", "'张三'");

        ////插入3条数据
        //sql.InsertValues("table1", new string[] { "3", "'王五'", "25", "'Wang5@163.com'" });
        //sql.InsertValues("table1", new string[] { "4", "'王五'", "26", "'Wang5@163.com'" });
        //sql.InsertValues("table1", new string[] { "5", "'王五'", "27", "'Wang5@163.com'" });

        //删除Name="王五"且Age=26的记录,DeleteValuesOR方法类似
        //sql.DeleteValuesAND("table1", new string[] { "Name", "Age" }, new string[] { "=", "=" }, new string[] { "'王五'", "'26'" });

        //读取整张表

        //SqliteDataReader reader = sql.ReadFullTable("table1");
        //while (reader.Read())
        //{
        //    //读取ID
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
        //    //读取Name
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        //    //读取Age
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("Age")));
        //    //读取Email
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Email")));
        //}

        ////读取数据表中Age>=25的所有记录的ID和Name
        //reader = sql.ReadTable(TalbleName, new string[] { "ID", "Name" }, new string[] { "Age" }, new string[] { ">=" }, new string[] { "'25'" });
        //while (reader.Read())
        //{
        //    //读取ID
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
        //    //读取Name
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        //}

        ////自定义SQL,删除数据表中所有Name="王五"的记录
        //sql.ExecuteQuery("DELETE FROM table1 WHERE NAME='王五'");

        ////关闭数据库连接
        //sql.CloseConnection();
    }


    #region  创建数据库
    //创建数据库,有则内置不创建
    public void CreateDatabase()
    {
        sql = new SQLiteHelper("Data");

    }
    #endregion

    #region 创建表
    //创建表,有则内置不创建
    public void CreateTable()
    {

        sql.OpenDatabase();
        sql.CreateTable(TalbleName, AllFields, new string[9] { "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" });
        sql.CloseDatabase();
    }
    #endregion

    #region 注册
    //插入数据(注册)
    public void Register(string[] AllData)
    {
        //if (QueryData(AllData[0])) return;
        sql.OpenDatabase();
        sql.InsertValues(TalbleName, AllData);
        sql.CloseDatabase();
    }
    #endregion

    #region 删除
    public void Delete(string SerialNumber)
    {
        sql.OpenDatabase();
        sql.DeleteValuesOR(TalbleName, new string[] { AllFields[0] }, new string[] { "==" }, new string[] { SerialNumber });
        sql.CloseDatabase();
    }
    #endregion

    #region 修改
   
    public void Change(string [] data)
    {
        sql.OpenDatabase();
        sql.UpdateValues(TalbleName, AllFields, data, AllFields[0],"==", data[0]);
        sql.CloseDatabase();
    }
    #endregion

    #region 查找
    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="SerialNumberValue">字段名</param>
    public void Seek(string SerialNumberValue)
    {
        if (SerialNumberValue.Trim() == "")
        {
            return;
        }
        //查找编号
        for (int i = 0; i < MDownEventManager.mTests[0].m_List_Txets.Count; i++)
        {
            //查找相对应的编号
            if (SerialNumberValue.Trim() == MDownEventManager.mTests[0].m_List_Txets[i].text)
            {

                RectTransform m_Rect_GridLayoutGroup = MDownEventManager.mGridLayoutGroup.GetComponent<RectTransform>();
                m_Rect_GridLayoutGroup.anchoredPosition = new Vector2(m_Rect_GridLayoutGroup.anchoredPosition.x, i * 35 + 35);

            }
        }
    }
    #endregion

    #region 确认这个编号是否存在
    /// <summary>
    /// 查询这个编号（数据的唯一标识）有返回True
    /// </summary>
    /// <param name="SerialNumber"></param>
    /// <returns></returns>
    public bool QueryData(string SerialNumberValue)
    {
        sql.OpenDatabase();
        SqliteDataReader reader = sql.ReadFullTable(TalbleName);
        try { reader = sql.ReadTable(TalbleName, new string[] { AllFields[0] }, new string[] { AllFields[0] }, new string[] { "==" }, new string[] { SerialNumberValue }); }
        catch (System.Exception) { }
        while (reader.Read())
        {
            //能读到证明这个编号存在，跳出
            sql.CloseDatabase();
            return true;
        }


        sql.CloseDatabase();
        return false;
    }
    #endregion

    #region 查询这个字段所包含的信息（一竖） 
    /// <summary>
    /// 查询每个字段，里面所有数据
    /// </summary>
    /// <param name="FieldName">字段名</param>
    /// <returns></returns>
    public List<string> FindField(string FieldName)
    {
        sql.OpenDatabase();
        List<string> Fields = new List<string>();

        //读取整张表
        SqliteDataReader reader = sql.ReadFullTable(TalbleName);
        while (reader.Read())
        {

            Fields.Add(reader.GetString(reader.GetOrdinal(FieldName)));
        }
        sql.CloseDatabase();
        return Fields;
    }
    #endregion

    #region 查询这个字段所包含的信息（一横的信息）
    public Dictionary<string, object> LoadData(string Field)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        sql.OpenDatabase();
        SqliteDataReader reader = sql.ReadFullTable(TalbleName);
        //读取数据表中Age>=25的所有记录的ID和Name
        reader = sql.ReadTable(TalbleName, AllFields, new string[] { AllFields[0] }, new string[] { "==" }, new string[] { Field });
        while (reader.Read())
        {
            //读取ID
            for (int i = 0; i < AllFields.Length; i++)
            {
                dic.Add(AllFields[i], reader.GetString(reader.GetOrdinal(AllFields[i]))); 
            }
           
            //读取Name
        }
        sql.CloseDatabase();
        return dic;
    }
    #endregion

    #region 更新Unity的表
    /// <summary>
    /// 更新Unity的表（增删查修都需要更新）
    /// </summary>
    public void FieldUpdataData()
    {
        List<string> list = FindField("SerialNumber");
        //当注册玩家数据的时候
        for (int i = MDownEventManager.mTests[0].m_List_Txets.Count; i < list.Count; i++)
        {
            MDownEventManager.AddData();
        }
        //当删除玩家数据的时候
        for (int i = MDownEventManager.mTests[0].m_List_Txets.Count; i > list.Count; i--)
        {
            MDownEventManager.DeleteDatas();
        }
        for (int i = 0; i < AllFields.Length; i++)
        {
            list = FindField(AllFields[i]);
            for (int j = 0; j < list.Count; j++)
            {
                MDownEventManager.mTests[i].m_List_Txets[j].text = list[j];
            }
        }
    }
    #endregion

   







    private void OnDestroy()
    {
        sql.CloseConnection();
    }






}
