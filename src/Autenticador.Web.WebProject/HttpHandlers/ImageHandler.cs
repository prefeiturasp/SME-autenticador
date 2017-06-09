using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using Autenticador.BLL;
using Autenticador.Entities;

namespace Autenticador.Web.WebProject.HttpHandlers
{
    /// <summary>
    /// Classe que implementa a interface IHttpHandler para visualizar imagens.
    /// Busca a imagem passada por parâmetro na pasta padrão de arquivos.
    /// Para chamar a página:
    ///     imagem.ashx?picture=imagem.jpg
    /// Para buscar uma imagem em uma subpasta, é só passar no nome da pasta junto com o
    ///     nome da imagem.
    ///     Ex: picture=Pasta\imagem.jpg
    ///
    /// Para visualizar a imagem com dimensões customizadas passar:
    ///     w=100 (Width - Largura)
    ///     h=200 (Height - Altura)
    ///
    /// Exemplo: imagem.ashx?picture=Pasta\imagem.jpg&w=100&h=200
    /// Pode ser utilizada também passando o arq_id da imagem solicitada. Ex:
    ///     imagem.ashx?id=10
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
        #region Constantes

        private static readonly List<string> extensoesValidas =
            new List<string> { ".png", ".jpg", ".jpeg" };

        const int size = 10000;

        #endregion Constantes

        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that
        /// implements the IHttpHandler interface.
        /// </summary>
        public void ProcessRequest(HttpContext context)
        {
            VerificaParametro_Picture(context);
            VerificaParametro_Id(context);
        }

        /// <summary>
        /// Busca o parâmetro "id" na queryString, e trata caso exista.
        /// </summary>
        /// <param name="context"></param>
        private void VerificaParametro_Id(HttpContext context)
        {
            if (!String.IsNullOrEmpty(context.Request.QueryString["id"]))
            {
                try
                {
                    string sArq_id = context.Server.UrlDecode(context.Request.QueryString["id"]);
                    long arq_id;

                    if ((Int64.TryParse(sArq_id, out arq_id)) && (arq_id > 0))
                    {
                        CFG_Arquivo arquivo = RetornaArquivoPorID(arq_id);
                        if ((!arquivo.IsNew) && (IsImage(Path.GetExtension(arquivo.arq_nome))))
                        {
                            
                            try
                            {
                                byte[] bufferData = arquivo.arq_data;

                                MemoryStream stream = new MemoryStream(bufferData);
                                Image img = Image.FromStream(stream);

                                context.Response.Clear();
                                context.Response.ContentType = arquivo.arq_typeMime;
                                context.Response.BinaryWrite(bufferData);
                                context.Response.Flush();

                                img.Dispose();
                                stream.Dispose();
                            }
                            catch (Exception ex)
                            {
                                ApplicationWEB._GravaErro(ex);
                                context.ApplicationInstance.CompleteRequest();
                                context.Server.ClearError();
                            }
                            finally
                            {
                                context.ApplicationInstance.CompleteRequest();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ApplicationWEB._GravaErro(ex);
                }
            }
        }

        /// <summary>
        /// Busca o parâmetro "picture" na queryString, e trata caso exista.
        /// </summary>
        /// <param name="context"></param>
        private void VerificaParametro_Picture(HttpContext context)
        {
            if (!String.IsNullOrEmpty(context.Request.QueryString["picture"]))
            {
                string fileName = UtilBO.DecodificaQueryString(context.Request.QueryString["picture"]);

                bool redimensionar = ((!String.IsNullOrEmpty(context.Request.QueryString["w"])) &&
                                      (!String.IsNullOrEmpty(context.Request.QueryString["h"])));

                try
                {
                    string folder = context.Server.MapPath("~/App_Themes");

                    FileInfo info = new FileInfo(folder + Path.DirectorySeparatorChar + fileName);

                    if (info.Directory.FullName.StartsWith(folder, StringComparison.OrdinalIgnoreCase))
                    {
                        if ((info.Exists) && IsImage(info))
                        {
                            context.Response.Cache.SetCacheability(HttpCacheability.Public);
                            context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));

                            if (redimensionar)
                            {
                                int heigth = Convert.ToInt32(context.Request.QueryString["h"]);
                                int width = Convert.ToInt32(context.Request.QueryString["w"]);

                                Image.GetThumbnailImageAbort myCallback = ThumbnailCallback;
                                Image img = Image.FromFile(info.FullName);

                                // Criando objeto redimencionado.
                                Image thumb = img.GetThumbnailImage
                                    (
                                        width,
                                        heigth,
                                        myCallback,
                                        IntPtr.Zero
                                    );

                                // ContentType sempre JPG.
                                context.Response.ContentType = "image/png";

                                // Enviando email para Response.
                                thumb.Save
                                    (
                                        context.Response.OutputStream,
                                        System.Drawing.Imaging.ImageFormat.Jpeg
                                    );

                                img.Dispose();
                                thumb.Dispose();
                            }
                            else
                            {
                                int index = fileName.LastIndexOf(".") + 1;
                                string extension = fileName.Substring(index).ToUpperInvariant();

                                // Fix for IE not handling jpg image types
                                if (string.Compare(extension, "JPG") == 0)
                                    context.Response.ContentType = "image/png";
                                else
                                    context.Response.ContentType = "image/" + extension;

                                context.Response.TransmitFile(info.FullName);
                            }

                            context.Response.Flush();
                        }
                        else
                        {
                            // Limpa o response.
                            context.Response.Flush();
                            context.Response.Clear();
                            context.ApplicationInstance.CompleteRequest();
                        }
                    }
                    else
                    {
                        context.Response.Redirect("~/Manutencao.aspx?erro=404", false);
                        context.ApplicationInstance.CompleteRequest();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        // Limpa o response.
                        context.Response.Flush();
                        context.Response.Clear();
                        context.ApplicationInstance.CompleteRequest();
                        ApplicationWEB._GravaErro(ex);
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion IHttpHandler Members

        #region Métodos

        /// <summary>
        /// Verifica pela extensão se é uma imagem válida.
        /// </summary>
        /// <param name="fi"></param>
        /// <returns></returns>
        private bool IsImage(FileInfo fi)
        {
            string ext = fi.Extension;

            return extensoesValidas.Exists(p => p.Equals(ext, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Verifica se é imagem pelo tipo de arquivo passado por parâmetro.
        /// </summary>
        /// <param name="extensao">Tipo de arquivo</param>
        /// <returns></returns>
        private bool IsImage(string extensao)
        {
            return extensoesValidas.Exists(p => p.Equals(extensao.ToLower().Replace("image/", ""), StringComparison.OrdinalIgnoreCase));
        }

        private bool ThumbnailCallback()
        {
            return true;
        }

        #region Métodos

        /// <summary>
        /// Retorna a entidade pelo ID.
        /// </summary>
        /// <param name="arq_id">ID do arquivo.</param>
        /// <returns></returns>
        private CFG_Arquivo RetornaArquivoPorID(long arq_id)
        {
            CFG_Arquivo arquivo = new CFG_Arquivo { arq_id = arq_id };
            arquivo = CFG_ArquivoBO.GetEntity(arquivo);

            return arquivo;
        }

        #endregion

        #endregion Métodos
    }
}