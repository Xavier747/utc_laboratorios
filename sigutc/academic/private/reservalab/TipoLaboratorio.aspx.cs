using LabUTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class academic_private_reservalab_TipoLaboratorio : System.Web.UI.Page
{
    public partial class TipoLaboratorio : System.Web.UI.Page
    {
        private string cadenaConexion;

        private string title;
        private string icon;

        LAB_TIPO tipoLaboratorio1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cedula"] == null) Response.Redirect("~/Views/Login.aspx");

            this.cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;

            tipoLaboratorio1 = new LAB_TIPO(cadenaConexion);

            if (!IsPostBack)
            {
                cargarTipoLaboratorio();
            }
        }

        public void cargarTipoLaboratorio()
        {
            string tipoConsulta = "ALL";
            tipoLaboratorio1.strCod_tipoLab = "";

            DataTable tablaDatos = tipoLaboratorio1.consultarTipoLaboratorio(tipoConsulta);
            try
            {
                if (tablaDatos != null && tablaDatos.Rows.Count > 0) // Verificar si tiene datos
                {
                    gvTipoLaboratorio.DataSource = tablaDatos;
                    gvTipoLaboratorio.DataBind();
                }

                lblMsgLstRegistros.Visible = tablaDatos != null && tablaDatos.Rows.Count > 0 ? false : true;
            }
            catch (Exception ex)
            {
                // Muestra un error si ocurre
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        protected void gvTipoLaboratorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTipoLaboratorio.PageIndex = e.NewPageIndex;
            cargarTipoLaboratorio();
        }

        protected void gvTipoLaboratorio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                tipoLaboratorio1.strCod_tipoLab = e.CommandArgument.ToString();

                // Carga los detalles del laboratorio según el ID seleccionado   
                tipoLaboratorio1.listadoLaboratorioId();

                // Llena el formulario de actualización con los datos cargados
                llenarFormActualizar();

                // Muestra el modal para actualizar los datos
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar').modal('show');", true);
            }
            if (e.CommandName == "Eliminar")
            {
                string tipoConsulta = "xCodTipoLab";
                tipoLaboratorio1.strCod_tipoLab = e.CommandArgument.ToString();
                tipoLaboratorio1.dtFecha_log = DateTime.Now;
                tipoLaboratorio1.strUser_log = Session["Cedula"].ToString();

                // Carga los detalles del laboratorio según el ID seleccionado   
                bool eliminar = tipoLaboratorio1.eliminarRelacion(tipoConsulta);

                title = eliminar == true ? "Registro eliminado correctamente." : "Registro no eliminado.";
                icon = eliminar == true ? "success" : "error";

                string script = $"showAlertAndReload('{title}', '{icon}');";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string codTipo = generarIdSoft();

            tipoLaboratorio1.strNombre_tipoLab = txtNombre.Text.ToUpper();
            tipoLaboratorio1.dtFechaRegistro_tipoLab = DateTime.Now;
            tipoLaboratorio1.dtFecha_log = DateTime.Now;
            tipoLaboratorio1.strUser_log = Session["Cedula"].ToString();
            tipoLaboratorio1.strCod_tipoLab = codTipo;

            bool registro = tipoLaboratorio1.crearSoftwareLaboratorio();

            title = registro == true ? "Los datos se han guardado correctamente." : "Los datos no se han guardado correctamente.";
            icon = registro == true ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }

        public string comprobarRegistro(string resultado)
        {
            string tipoConsulta = "xEstado";
            int acum = 0;

            while (true)
            {
                tipoLaboratorio1.strCod_tipoLab = resultado;
                DataTable tablaDatos = tipoLaboratorio1.consultarTipoLaboratorio(tipoConsulta);

                if (tablaDatos.Rows.Count > 0)
                {
                    ++acum;
                    resultado = resultado + "_" + acum;
                }
                else
                {
                    break;
                }
            }

            return resultado;
        }
        private string generarIdSoft()
        {
            string frase = txtNombre.Text.ToUpper();
            string[] palabras = frase.Split(' ');

            List<string> partes = new List<string>();

            foreach (string palabra in palabras)
            {
                // Si tiene más de 5 caracteres, tomamos solo los primeros 5
                string parte = palabra.Length > 3 ? palabra.Substring(0, 3) : palabra;
                partes.Add(parte);
            }

            string resultado = string.Join("", partes);
            string codTipo = comprobarRegistro(resultado);

            return codTipo;
        }

        public void llenarFormActualizar()
        {
            lblCodeTipoLabAct.Text = tipoLaboratorio1.strCod_tipoLab;
            txtNombreAct.Text = tipoLaboratorio1.strNombre_tipoLab;
        }

        protected void btn_Actualizar_Click(object sender, EventArgs e)
        {
            tipoLaboratorio1.strCod_tipoLab = lblCodeTipoLabAct.Text;
            tipoLaboratorio1.strNombre_tipoLab = txtNombreAct.Text;
            tipoLaboratorio1.dtFecha_log = DateTime.Now;
            tipoLaboratorio1.strUser_log = Session["Cedula"].ToString();

            bool actualizar = tipoLaboratorio1.actualizarTipoLaboratorio();

            title = actualizar == true ? "Los datos se han actualizado correctamente." : "Los datos no se han actualizado correctamente.";
            icon = actualizar == true ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
    }
