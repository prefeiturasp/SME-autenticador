using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace AutenticadorWebSite
{
    /// <summary>
    /// Summary description for Captcha
    /// </summary>
    public class Captcha : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                bool sujeira = true;
                Bitmap objetoBMP = new Bitmap(80, 25);
                Graphics graphics = Graphics.FromImage(objetoBMP);
                graphics.Clear(Color.WhiteSmoke);
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Fonte configurada para ser usada no texto do captcha
                Font fonte = new Font("Arial Black", 13, FontStyle.Bold);

                //Valores possíveis para a string
                string caracteres = "ABCDEFGHIJKLMNOPQRSTUVXYWZ0123456789";

                //Cria o valor randomicamente e adiciona ao array
                Random random = new Random();
                StringBuilder captchaValue = new StringBuilder();

                for (int i = 0; i < 4; i++)
                {
                    captchaValue.Append(caracteres[(int)(random.NextDouble() * caracteres.Length)] + " ");
                }


                //Adiciona o valor gerado para o captcha na sessão
                //para ser validado posteriormente
                context.Session.Add("CaptchaValue", captchaValue.ToString().Replace(" ", ""));

                //Adiciona Sujeira no fundo
                if (sujeira) {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    var rand = new Random((int)DateTime.Now.Ticks);
                    for (i = 1; i < 20; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (80 / 3));
                        x = rand.Next(0, 80);
                        y = rand.Next(0, 25);
                        graphics.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //Desenha a imagem com o texto
                graphics.DrawString(captchaValue.ToString().TrimEnd(), fonte, Brushes.Black, 0, 0);

                //Determina o tipo de conteúdo da imagem do captcha
                context.Response.ContentType = "image/GIF";

                //Salva em stream
                objetoBMP.Save(context.Response.OutputStream, ImageFormat.Gif);

                //Libera os objeto da memória pois os mesmos não são mais necessários
                fonte.Dispose();
                graphics.Dispose();
                objetoBMP.Dispose();
            }
            catch (Exception)
            {
                
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
}