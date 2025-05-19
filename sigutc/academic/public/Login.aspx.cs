using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class academic_private_Login : System.Web.UI.Page
{
    string cadenaConexion;
    SqlConnection conexion;

    string rol = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.cadenaConexion = ConfigurationManager.AppSettings["conexionBddProductos"];
        this.conexion = new SqlConnection(this.cadenaConexion);

        if (!IsPostBack)
        {
            // Almacenar la URL de retorno en una variable de sesión en lugar de concatenarla
            string returnUrl = Request.QueryString["ReturnUrl"];
            if (!string.IsNullOrEmpty(returnUrl))
            {
                Session["ReturnUrl"] = returnUrl;
            }
        }
    }
    protected void LogIn(object sender, EventArgs e)
    {
        if (IsValid)
        {
            string usuario = Usuario.Text;
            string password = Password.Text;

            if (iniarSecion(usuario, password))
            {
                FormsAuthentication.SetAuthCookie(usuario, true);

                string returnUrl = Request.QueryString["ReturnUrl"] as string;
                if (string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect("~/academic/private/Default.aspx");
                }
                else
                {
                    Response.Redirect(returnUrl);
                    Session.Remove("ReturnUrl"); // Limpiar después de usar
                }
            }
        }
    }

    /*Metodo con parametros para iniciar sesion y con retorno booleano*/
    public bool iniarSecion(string usuario, string password)
    {
        bool validacion = false;
        /*llamado al sp*/
        SqlCommand comandoConsulta = new SqlCommand("loginPlataforma", this.conexion);
        /*Envio de parametros*/
        comandoConsulta.Parameters.AddWithValue("@Comodin", "xUserPassword");
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", usuario);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", password);
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", "");
        
        comandoConsulta.CommandType = CommandType.StoredProcedure;
        try
        {
            this.conexion.Open();
            SqlDataReader dr = comandoConsulta.ExecuteReader();
            /*retorno de parametros una vez validado*/
            if (dr.Read())
            {
                validacion = true;
            }
            dr.Close();
            conexion.Close();
        }
        catch (Exception ex)
        {
            FailureText.Text = "ERROR: " + ex.Message;
            ErrorMessage.Visible = true;
            validacion = false;
        }
        return validacion;
    }
}