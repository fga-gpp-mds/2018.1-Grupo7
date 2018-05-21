﻿using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


/**
 * Descrever aqui o que essa classe realiza.
 */
public class UDPClient : MonoBehaviour
{
    // broadcast address
    private string host = "127.0.0.1";
    private int port = 5005;
    private UdpClient client;

    public Transform mao, cotovelo, ombro, braco; //o ponto final de mao é o inicial de cotovelo, o final de cotovelo é o inicial de ombro; ou seja, sao apenas 2 retas
                                                  //	List<Vector2> tempo_anguloDeJunta;
    float current_time_movement = 0;

    void Start()
    {
        client = new UdpClient();
        client.Connect(host, port);
    }


    /**
	 * Descrever aqui o que esse método realiza.
	 */
    void FixedUpdate()
    {
       
        current_time_movement += Time.fixedDeltaTime;

        StringBuilder sb = new StringBuilder();
        sb.Append(current_time_movement).Append(" ");

        sb.Append(mao.position.x).Append(" ").Append(mao.position.y).Append(" ").Append(mao.position.z).Append(" ");
        sb.Append(mao.rotation.x).Append(" ").Append(mao.rotation.y).Append(" ").Append(mao.rotation.z).Append(" ");

        sb.Append(cotovelo.position.x).Append(" ").Append(cotovelo.position.y).Append(" ").Append(cotovelo.position.z).Append(" ");
        sb.Append(cotovelo.rotation.x).Append(" ").Append(cotovelo.rotation.y).Append(" ").Append(cotovelo.rotation.z).Append(" ");

        sb.Append(ombro.position.x).Append(" ").Append(ombro.position.y).Append(" ").Append(ombro.position.z).Append(" ");
        sb.Append(ombro.rotation.x).Append(" ").Append(ombro.rotation.y).Append(" ").Append(ombro.rotation.z).Append(" ");

        sb.Append(braco.position.x).Append(" ").Append(braco.position.y).Append(" ").Append(braco.position.z).Append(" ");
        sb.Append(braco.rotation.x).Append(" ").Append(braco.rotation.y).Append(" ").Append(braco.rotation.z).Append("\n");

        byte[] dgram = Encoding.UTF8.GetBytes(sb.ToString());
        client.Send(dgram, dgram.Length);
     
    }

    void OnApplicationQuit()
    {
        client.Close();
    }
}
