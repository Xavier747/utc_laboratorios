using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryLaboratorios;

public partial class academic_private_reservalab_LaboratorioCarrera : System.Web.UI.Page
{
    public partial class LaboratorioCarrera : System.Web.UI.Page
    {
        private string title;
        private string icon;

        private string cadenaConexion;
        private SqlConnection conexion;

        LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
        LAB_EXCLUSIVO labExc1= new LAB_EXCLUSIVO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cedula"] == null) Response.Redirect("~/Views/Login.aspx");

            this.cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;
            this.conexion = new SqlConnection(this.cadenaConexion);

            if (!IsPostBack)
            {
                cargarLaboratorio();
                cargarCarreras();
                cargarCarrerasExclusivas();
            }
        }

        public void cargarLaboratorio()
        {
            laboratorio2.strCod_Lab = Session["laboratorioId"].ToString();
            //laboratorio2.listarLaboratorioPorId();
            //lblFacultadId.Text = laboratorio2.strCod_Fac;
            //lblSedeId.Text = laboratorio2.strCod_Sede;

            //nombreLboratorio.InnerText = laboratorio2.strNombre_Lab.ToUpper();
        }

        public void cargarCarreras()
        {
            string tipoConsulta = "xCodLaboratorio";
            string codLab = Session["laboratorioId"].ToString();

            SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetUB_CARRERAS", conexion);
            comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
            //comandoConsulta.Parameters.AddWithValue("@FILTRO1", lblFacultadId.Text);
            //comandoConsulta.Parameters.AddWithValue("@FILTRO2", lblSedeId.Text);
            comandoConsulta.Parameters.AddWithValue("@FILTRO3", "");
            comandoConsulta.Parameters.AddWithValue("@FILTRO4", "");
            comandoConsulta.CommandType = CommandType.StoredProcedure;
            try
            {
                this.conexion.Open();
                SqlDataAdapter adaptadorAlbum = new SqlDataAdapter(comandoConsulta);
                DataTable dt = new DataTable();
                adaptadorAlbum.Fill(dt);

                //    ddlCarreras.Items.Clear();

                //    ddlCarreras.Items.Add(new ListItem("-- Seleccione una opcion --", ""));

                //    foreach (DataRow row in dt.Rows)
                //    {
                //        ddlCarreras.Items.Add(new ListItem(row["strNombre_Car"].ToString(), row["strCod_Car"].ToString()));
                //    }
            }
            catch (Exception ex)
            {
                Response.Write("TIENES UN ERROR: " + ex.Message);
            }
            conexion.Close();
        }

        public void cargarCarrerasExclusivas()
        {
            string tipoConsulta = "xLabExclusivo";
            string filtro = Session["laboratorioId"].ToString();

            //DataTable labExc = labExc1.obtenerLaboratorios(tipoConsulta, filtro);

            try
            {
                //if (labExc != null && labExc.Rows.Count > 0) // Verificar si tiene datos
                //{
                //    gvCarreras.DataSource = labExc;
                //    gvCarreras.DataBind();
                //    gvCarreras.AllowPaging = false;
                //}

                //lblMsgLstRegistros.Visible = labExc != null && labExc.Rows.Count > 0 ? false : true;
            }
            catch (Exception ex)
            {
                // Muestra un error si ocurre
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string tipoConsulta = "xCarreraLabExc";

            //labExc1.strCod_Car = ddlCarreras.SelectedValue;
            ////int validar = labExc1.validarCarreraUnico(tipoConsulta);

            //if (validar == 0)
            //{
            //    Random rand = new Random();
            //    int num = rand.Next(0, 1000);

            //    laboratorio2.strCod_Lab = Session["laboratorioId"].ToString();
            //    laboratorio2.listarLaboratorioPorId();

            //    labExc1.dtFechaRegistro_labEx = DateTime.Now;
            //    labExc1.dtFecha_log = DateTime.Now;
            //    labExc1.strUser_log = Session["Cedula"].ToString();
            //    labExc1.strCod_lab = laboratorio2.strCod_Lab;
            //    //labExc1.strCod_Car = ddlCarreras.SelectedValue;
            //    //labExc1.strCod_Fac = laboratorio2.strCod_Fac;
            //    //labExc1.strCod_Sede = laboratorio2.strCod_Sede;
            //    //labExc1.strCod_labEx = laboratorio2.strCod_Sede + '_' + laboratorio2.strCod_Fac + '_' + ddlCarreras.SelectedValue + '_' + num;

            //    bool registro = labExc1.registrarLaboratorioExclusivo();

            //    title = registro == true ? "Los datos se han guardado correctamente." : "Los datos no se han guardado correctamente.";
            //    icon = registro == true ? "success" : "error";
            //}
            //else
            //{
            //    title = "La carrera ya se encuentra relaciodado en este laboratorio.";
            //    icon = "error";
            //}

            //string script = $"showAlertAndReload('{title}', '{icon}');";
            //ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }

        protected void gvCarreras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                labExc1.strCod_labEx = e.CommandArgument.ToString();
                labExc1.dtFecha_log = DateTime.Now;
                labExc1.strUser_log = Session["Cedula"].ToString();
                //bool eliminar = labExc1.eliminarCarrera();

                //title = eliminar == true ? "Registro eliminado correctamente." : "Registro no eliminado.";
                //icon = eliminar == true ? "success" : "error";

                //string script = $"showAlertAndReload('{title}', '{icon}');";
                //ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {

    }

    protected void gvCarreras_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}