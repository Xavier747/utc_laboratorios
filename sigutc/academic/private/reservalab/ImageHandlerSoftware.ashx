<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using System.IO;

public class ImageHandler : IHttpHandler {
    public bool IsReusable { get { return false; } }
    
    public void ProcessRequest (HttpContext context) {
        string imageName = context.Request.QueryString["image"];
        if (!string.IsNullOrEmpty(imageName) && Path.GetFileName(imageName) == imageName)
        {
            string imagePath = Path.Combine(@"C:\images\Laboratorios", imageName);
            if (File.Exists(imagePath))
            {
                string mimeType = System.Web.MimeMapping.GetMimeMapping(imagePath);
                context.Response.ContentType = mimeType;
                byte[] imageContent = File.ReadAllBytes(imagePath);
                context.Response.OutputStream.Write(imageContent, 0, imageContent.Length);
            }
            else
            {
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "Imagen no encontrada";
            }
        }
        else
        {
            context.Response.StatusCode = 400;
            context.Response.StatusDescription = "Solicitud inválida";
        }
    }
}