using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using ClassLibraryLaboratorios;

public partial class academic_private_reservalab_Laboratorios : System.Web.UI.Page
{
    public partial class Laboratorios : System.Web.UI.Page
    {
        string cadenaConexion;
        SqlConnection conexion;

        LAB_LABORATORIOS laboratorio2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cedula"] == null) Response.Redirect("~/Views/Login.aspx");

            this.cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;
            this.conexion = new SqlConnection(this.cadenaConexion);

            laboratorio2 = new LAB_LABORATORIOS(cadenaConexion);

            if (!IsPostBack)
            {
                cargarFacultad();
                cargarTabla();
            }
        }

        public void cargarFacultad()
        {
            SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetUB_FACULTADES", conexion);
            string tipoConsulta = "xIdPersonal";
            string personalId = Session["Cedula"].ToString();

            comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
            comandoConsulta.Parameters.AddWithValue("@FILTRO1", personalId);
            comandoConsulta.Parameters.AddWithValue("@FILTRO2", "");
            comandoConsulta.Parameters.AddWithValue("@FILTRO3", "");
            comandoConsulta.Parameters.AddWithValue("@FILTRO4", "");
            comandoConsulta.CommandType = CommandType.StoredProcedure;
            try
            {
                conexion.Open();
                SqlDataAdapter adaptadorAlbum = new SqlDataAdapter(comandoConsulta);
                DataTable dt = new DataTable();
                adaptadorAlbum.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    laboratorio2.strCod_Fac = Convert.ToString(dt.Rows[0]["strCod_Fac"]);
                    //lblIdFacultad.Text = Convert.ToString(dt.Rows[0]["strCod_Fac"]);
                }

                //rptFacultades.DataSource = dt;
                //rptFacultades.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("TIENES UN ERROR: " + ex.Message);
            }
            conexion.Close();
        }

        protected void rptFacultades_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CargarLaboratorios")
            {
                laboratorio2.strCod_Fac = Convert.ToString(e.CommandArgument);
                //lblIdFacultad.Text = Convert.ToString(e.CommandArgument);

                ViewState["FacultadSeleccionada"] = Convert.ToString(e.CommandArgument);

                //txtSearch.Text = "";
                cargarTabla();
            }
        }

        public void cargarTabla()
        {
            try
            {
                string comodin = "xFacultad";
                string filtro = "";
                DataTable tablaDatos = laboratorio2.obtenerLaboratorios(comodin, filtro);

                //listarLaboratorios.DataSource = tablaDatos;
                //listarLaboratorios.DataBind();
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //string filtro = txtSearch.Text.Trim();
            //cargarTablaFiltrada(filtro);
        }


        public void cargarTablaFiltrada(string filtro)
        {
            try
            {
                string comodin = "xFacultad";
                DataTable tablaDatos = laboratorio2.obtenerLaboratorios(comodin, filtro);

                //listarLaboratorios.DataSource = tablaDatos;
                //listarLaboratorios.DataBind();
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }


        protected void listarLaboratorios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Reservar")
            {
                Session["laboratorioId"] = e.CommandArgument.ToString();
                Response.Redirect("~/Views/Docentes/ReservaLaboratorio.aspx");
            }
            else if (e.CommandName == "Informacion")
            {
                Session["laboratorioId"] = e.CommandArgument.ToString();
                Response.Redirect("~/Views/Docentes/InformacionLaboratorios.aspx");
            }
        }
    }

    protected void rptFacultades_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void listarLaboratorios_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

    }
}