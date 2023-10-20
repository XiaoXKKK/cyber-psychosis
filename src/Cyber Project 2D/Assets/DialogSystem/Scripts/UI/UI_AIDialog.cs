using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_AIDialog : MonoBehaviour
{
    public GameObject npcController;

    private ProcessStartInfo startInfo;
    private Process process;

    private UdpClient udpClient;
    private IPEndPoint remoteEP;

    private InputField input;

    // Start is called before the first frame update
    void Start()
    {
        input = transform.Find("InputField").GetComponent<InputField>();

        Kill_All_Python_Process();

        // ����UDPͨ�ŵ�Client
        udpClient = new UdpClient();
        // ����IP��ַ��˿ں�
        remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31415);

        UDPManager manager = GetComponent<UDPManager>();
        if (manager == null)
        {
            manager = gameObject.AddComponent<UDPManager>();
        }
        //���úý����ķ���
        manager.SetReceiveCallBack(ReceiveUDPMessage);

        StartSubProcess();
    }

    [Serializable]
    public class PythonJsonData
    {
        public string content;
        public int score;
    }

    [Serializable]
    public class UnityJsonData
    {
        public string content;
        public string name;
        public int now_state;
    }
    public void Send()
    {
        if (NewDaySystem.Instance.AItimes > 0)
        {
            NewDaySystem.Instance.AItimes--;
        }
        else
        {
            UI_Dialog.Instance.InitDialog(UI_Dialog.Instance.aiend);
            return;
        }
        UnityEngine.Debug.Log(input.text);
        NPC_Base curr = UI_Dialog.Instance.Currnpc;
        UnityJsonData jsonData = new()
        {
            content = input.text,
            name = curr.npcname,
            now_state = curr.favorability
        };
        byte[] message = Encoding.UTF8.GetBytes(JsonUtility.ToJson(jsonData));
        udpClient.Send(message, message.Length, remoteEP);
    }

    void ReceiveUDPMessage(string receiveData)
    {
        //�������Ϳ��Լ���ν�����
        UnityEngine.Debug.Log(receiveData);
        PythonJsonData data = JsonUtility.FromJson<PythonJsonData>(receiveData);
        UI_Dialog.Instance.SaySth(data.content);
        UI_Dialog.Instance.UpdateScore(data.score);
    }

    private void StartSubProcess()
    {
        // ����python���ļ���
        string fileName = "gpt_test.py";
        // ��ȡUnity��Ŀ������·��
        string dataPath = Application.dataPath;
        // ƴ��Python�ļ�������·��
        string pythonPath = dataPath + "/" + "Python";
        string fullPath = pythonPath + "/" + fileName;
        // ���������в���
        string command = "/c python \"" + fullPath + "\"";

        // ����ProcessStartInfo����
        startInfo = new ProcessStartInfo();
        // �趨ִ��cmd
        startInfo.FileName = "cmd.exe";
        // ���ù���Ŀ¼
        startInfo.WorkingDirectory = pythonPath;
        // �����������һ����command�ַ���
        startInfo.Arguments = command;
        // ��ΪǶ��Unity�к�̨ʹ�ã��������ò���ʾ����
        startInfo.CreateNoWindow = true;
        // ������Ҫ�趨Ϊfalse��ʹ��CreateProcess�������̣�
        startInfo.UseShellExecute = false;

        // ����Process
        process = new Process();
        process.StartInfo = startInfo;

        //�����ű�Process�����Ҽ������ж�ȡ����뱨��
        process.Start();
    }

    void Kill_All_Python_Process()
    {
        Process[] allProcesses = Process.GetProcesses();
        foreach (Process process_1 in allProcesses)
        {
            try
            {
                // ��ȡ���̵�����
                string processName = process_1.ProcessName;
                // ������������а���"python"������ֹ�ý���
                if (processName.ToLower().Contains("python") && process_1.Id != Process.GetCurrentProcess().Id)
                {
                    process_1.Kill();
                }
            }
            catch (Exception ex)
            {
                // �����쳣
                print(ex);
            }
        }
    }
    void OnApplicationQuit()
    {
        // ��Ӧ�ó����˳�ǰִ��һЩ����
        UnityEngine.Debug.Log("Ӧ�ó��򼴽��˳�����������Python����");
        // ��������Python����
        Kill_All_Python_Process();
    }
}