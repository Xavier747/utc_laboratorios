using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using ClassLibraryLaboratorios;
using System.Web.Configuration;

public partial class academic_private_reservalab_InformacionLaboratorios : System.Web.UI.Page
{
    string cadenaConexion;
    SqlConnection conexion;

    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();
    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    LAB_TIPO tipo1 = new LAB_TIPO();
    Personal personal1 = new Personal();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;
        this.conexion = new SqlConnection(this.cadenaConexion);

        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            cargarTabla();
        }
    }

    public void cargarTabla()
    {
        string strCod_lab = Session["laboratorioId"].ToString();

        var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", strCod_lab, "", "", "");

        nombre.InnerText = listLaboratorio[0].strNombre_lab;
        txtUbicacion.InnerText = listLaboratorio[0].strUbicacion_lab;

        DataList1.DataSource = listLaboratorio;
        DataList1.DataBind();

        DataList2.DataSource = listLaboratorio;
        DataList2.DataBind();
    }
}

    class Personal
    {
        public string CEDULA_ALU { get; set; }
        public string NOMBRE_ALU { get; set; }
        public string CORREO_ALU { get; set; }
        public string IMAGEN_ALU { get; set; }

        public Personal() { }

        public List<Personal> LoadPersonal(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
        {
            var listPersonal = new List<Personal>();

            SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);
            SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetPERSONAL", conexion);
            comandoConsulta.Parameters.AddWithValue("@Comodin", comodin);
            comandoConsulta.Parameters.AddWithValue("@FILTRO1", filtro1);
            comandoConsulta.Parameters.AddWithValue("@FILTRO2", filtro2);
            comandoConsulta.Parameters.AddWithValue("@FILTRO3", filtro3);
            comandoConsulta.Parameters.AddWithValue("@FILTRO4", filtro4);
            comandoConsulta.CommandType = CommandType.StoredProcedure;
            try
            {
                conexion.Open();
                SqlDataAdapter adaptadorAlbum = new SqlDataAdapter(comandoConsulta);
                DataTable dt = new DataTable();
                adaptadorAlbum.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    listPersonal.Add(
                        new Personal
                        {
                            CEDULA_ALU = row["CEDULA_ALU"].ToString(),
                            NOMBRE_ALU = row["APELLIDO_ALU"].ToString() + " " + row["APELLIDOM_ALU"].ToString() + " " + row["NOMBRE_ALU"].ToString(),
                            IMAGEN_ALU = row["IMAGEN_ALU"].ToString(),
                            CORREO_ALU = row["CORREO_ALU"].ToString()
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                Console.Write("TIENES UN ERROR: " + ex.Message);
            }
            conexion.Close();
            return listPersonal;
        }
}