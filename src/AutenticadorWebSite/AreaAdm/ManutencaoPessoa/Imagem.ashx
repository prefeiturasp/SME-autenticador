<%@ WebHandler Language="C#" Class="Imagem" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autenticador.Entities;
using Autenticador.BLL;

public class Imagem : IHttpHandler 
{     
    public void ProcessRequest (HttpContext context) 
    { 
        context.Response.Clear(); 
        if (!String.IsNullOrEmpty(context.Request.QueryString["id"])) 
        {
            Guid id = new Guid(context.Request.QueryString["id"]); 
            PES_Pessoa pes = new PES_Pessoa()
            {
                pes_id = id
            };
            PES_PessoaBO.GetEntity(pes);

            if (pes.pes_foto != null)
            {
                context.Response.ContentType = "image/jpeg";
                context.Response.OutputStream.Write(pes.pes_foto, 0, pes.pes_foto.Length - 1);                
            }
        }         
    } 
    public bool IsReusable 
    { 
        get 
        { 
            return false; 
        } 
   }       
}