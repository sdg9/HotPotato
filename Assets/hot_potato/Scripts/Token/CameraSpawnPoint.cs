﻿using UnityEngine;
using System.Collections;

public class CameraSpawnPoint : Bolt.IProtocolToken {

    //public float[] position;
    //Vector3 position;
    public Vector3 position;

    public CameraSpawnPoint()
    {

    }
    public CameraSpawnPoint(Vector3 position)
    {
        //this.position = new float[2]{position.x, position.y};
        this.position = new Vector3(position.x, position.y, position.z);
    }

    void Bolt.IProtocolToken.Read(UdpKit.UdpPacket packet)
    {
        position = packet.ReadVector3();
        //Number = packet.ReadInt();
    }

    void Bolt.IProtocolToken.Write(UdpKit.UdpPacket packet)
    {
        packet.WriteVector3(position);
    }

    public override string ToString()
    {
        return string.Format("[CameraSpawnPoint {0}]", position);
    }
}