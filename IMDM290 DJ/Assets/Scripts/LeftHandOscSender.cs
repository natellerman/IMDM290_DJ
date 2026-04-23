using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

[DisallowMultipleComponent]
public class LeftHandOscSender : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] private MediaPipeBodyTracker tracker;

    [Header("OSC Target")]
    [SerializeField] private string remoteHost = "127.0.0.1";
    [SerializeField] private int remotePort = 9000;
    [SerializeField] private string address = "/lefthand";

    [Header("Send Settings")]
    [SerializeField] private bool sendWhenNotTracked = false;
    [SerializeField, Min(0.001f)] private float sendInterval = 0.02f;
    [SerializeField] private bool verboseLogging = false;

    private UdpClient udpClient;
    private float nextSendTime;

    private void OnEnable()
    {
        ResolveTracker();
        CreateClient();
    }

    private void Update()
    {
        if (Time.unscaledTime < nextSendTime)
        {
            return;
        }

        nextSendTime = Time.unscaledTime + sendInterval;

        ResolveTracker();
        if (tracker == null)
        {
            return;
        }

        if (!sendWhenNotTracked && !tracker.LeftHandTracked)
        {
            return;
        }

        SendVector3(tracker.LeftHandPosition);
    }

    private void OnDisable()
    {
        DisposeClient();
    }

    private void OnValidate()
    {
        remotePort = Mathf.Clamp(remotePort, 1, 65535);
        sendInterval = Mathf.Max(0.001f, sendInterval);

        if (string.IsNullOrWhiteSpace(address))
        {
            address = "/leftHandPosition";
        }
        else if (!address.StartsWith("/"))
        {
            address = "/" + address.TrimStart('/');
        }
    }

    private void ResolveTracker()
    {
        if (tracker == null)
        {
            tracker = FindObjectOfType<MediaPipeBodyTracker>();
        }
    }

    private void CreateClient()
    {
        if (udpClient != null)
        {
            return;
        }

        try
        {
            udpClient = new UdpClient();
            udpClient.Connect(remoteHost, remotePort);
        }
        catch (Exception exception)
        {
            Debug.LogWarning($"{nameof(LeftHandOscSender)} failed to connect to {remoteHost}:{remotePort}. {exception.Message}", this);
            DisposeClient();
        }
    }

    private void DisposeClient()
    {
        if (udpClient == null)
        {
            return;
        }

        udpClient.Dispose();
        udpClient = null;
    }

    private void SendVector3(Vector3 value)
    {
        CreateClient();
        if (udpClient == null)
        {
            return;
        }

        var packet = BuildOscMessage(address, value.x, value.y, value.z);

        try
        {
            udpClient.Send(packet, packet.Length);

            if (verboseLogging)
            {
                Debug.Log($"{nameof(LeftHandOscSender)} sent {address} {value}", this);
            }
        }
        catch (Exception exception)
        {
            Debug.LogWarning($"{nameof(LeftHandOscSender)} failed to send OSC packet. {exception.Message}", this);
            DisposeClient();
        }
    }

    private static byte[] BuildOscMessage(string oscAddress, float x, float y, float z)
    {
        using (var stream = new MemoryStream())
        {
            WritePaddedString(stream, oscAddress);
            WritePaddedString(stream, ",fff");
            WriteBigEndianFloat(stream, x);
            WriteBigEndianFloat(stream, y);
            WriteBigEndianFloat(stream, z);
            return stream.ToArray();
        }
    }

    private static void WritePaddedString(Stream stream, string value)
    {
        var bytes = Encoding.ASCII.GetBytes(value ?? string.Empty);
        stream.Write(bytes, 0, bytes.Length);
        stream.WriteByte(0);

        while ((stream.Length % 4) != 0)
        {
            stream.WriteByte(0);
        }
    }

    private static void WriteBigEndianFloat(Stream stream, float value)
    {
        var bytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }

        stream.Write(bytes, 0, bytes.Length);
    }
}
