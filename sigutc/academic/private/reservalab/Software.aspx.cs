using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryTesis;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;
using System.Web.Services;

public partial class academic_private_reservaLab_Software : System.Web.UI.Page
{
    private string cadenaConexion;
    private SqlConnection conexion;

    private string title;
    private string icon;
    private decimal precioUnitario;

    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    UB_SEDES sede = new UB_SEDES();
    UB_FACULTADES facultad = new UB_FACULTADES();

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["Cedula"] == null) Response.Redirect("~/Views/Login.aspx");

        if (!IsPostBack)
        {
            cargarTipoLicencia();
            cargarSede();
            cargarFacultad();
            cargarSoftware();
        }
        
    }

    protected void ddlSedeSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultadSoft.Items.Clear();
        cargarFacultad();
        cargarSoftware();
    }

    protected void ddlFacultadSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSoftware.DataSource = null;
        cargarSoftware();
    }

    protected void gvSoftware_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSoftware.PageIndex = e.NewPageIndex;
    }

    public void cargarSede()
    {
        var listSede = sede.LoadUB_SEDES("ALL", "", "", "", "");
        if (listSede.Count != 0)
        {
            //ddlSede de listar Software
            ddlSedeSoft.DataSource = listSede;
            ddlSedeSoft.DataTextField = "strNombre_Sede";
            ddlSedeSoft.DataValueField = "strCod_Sede";
            ddlSedeSoft.DataBind();

            //ddlSede de modal nuevo Software
            ddlSede.DataSource = listSede;
            ddlSede.DataTextField = "strNombre_Sede";
            ddlSede.DataValueField = "strCod_Sede";
            ddlSede.DataBind();

            //ddlSedeAct
            ddlSedeAct.DataSource = listSede;
            ddlSedeAct.DataTextField = "strNombre_Sede";
            ddlSedeAct.DataValueField = "strCod_Sede";
            ddlSedeAct.DataBind();

            lblMsg.Text = sede.msg;
        }
        else
        {
            lblMsg.Text = sede.msg;
        }
    }

    public void cargarFacultad()
    {
        string strCod_Sede = ddlSedeSoft.SelectedValue;
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        if (listFacultad.Count != 0)
        {
            ddlFacultadSoft.DataSource = listFacultad;
            ddlFacultadSoft.DataTextField = "strNombre_fac";
            ddlFacultadSoft.DataValueField = "strCod_fac";
            ddlFacultadSoft.DataBind();



            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    public void cargarSoftware()
    {
        //lblMsgLstUsuario.Visible = false;
        string strCod_Fac = ddlFacultadSoft.SelectedValue;
        string strCod_Sede = ddlSedeSoft.SelectedValue;

        var listSoftware = software1.LoadLAB_SOFTWARE("xSedeFacultad", strCod_Fac, strCod_Sede, "", "");

        if (listSoftware.Count != 0)
        {
            gvSoftware.DataSource = listSoftware;
            gvSoftware.DataBind();
            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultad.Items.Clear();
        cargarFacultad();
    }

    protected void ddlSedeAct_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultadAct.Items.Clear();        
    }

    public void cargarTipoLicencia()
    {
        ddlTipo.Items.Add(new ListItem("-- Seleccione una opcion --", ""));
        ddlTipo.Items.Add(new ListItem("Libre", "Libre"));
        ddlTipo.Items.Add(new ListItem("Propietario", "Propietario"));

        ddlTipoAct.Items.Add(new ListItem("-- Seleccione una opcion --", ""));
        ddlTipoAct.Items.Add(new ListItem("Libre", "Libre"));
        ddlTipoAct.Items.Add(new ListItem("Propietario", "Propietario"));
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        precioUnitario = txtCosto.Text != "" ? decimal.Parse(txtCosto.Text) : 0;

        software1.strNombre_sof = txtNombre.Text;
        software1.strTipoLicencia_sof = ddlTipo.SelectedValue;
        software1.strNombreLicencia_sof = txtNombreLicencia.Text;
        software1.intCantidad_sof = int.Parse(txtCantidad.Text);
        software1.decCostoUnitario_sof = precioUnitario;
        software1.decCostoTotal_sof = decimal.Parse(txtCantidad.Text) * precioUnitario;
        software1.strDescripcion_sof = txtDescripcion.Text;
        software1.strUrl_sof = txtLink.Text;
        software1.dtFechaRegistro_sof = DateTime.Now;
        software1.dtFecha_log = DateTime.Now;
        software1.strUser_log = Session["Cedula"].ToString();

        //string codSoft = generarIdSoft();

        software1.strCod_Fac = ddlFacultad.SelectedValue;
        software1.strCod_Sede = ddlSede.SelectedValue;

        //software1.strCod_sof = codSoft;

        if (fulImg1.HasFile)
        {
            try
            {
                string folderPath = Server.MapPath("~/images/Software/");
                string filename = Path.GetFileNameWithoutExtension(fulImg1.FileName);
                string extension = Path.GetExtension(fulImg1.FileName);
                string newFilename = filename + extension;
                string path = Path.Combine(folderPath, newFilename);

                // Verificar si el archivo existe y agregar un sufijo numérico
                int counter = 1;
                while (File.Exists(path))
                {
                    newFilename = $"{filename}_{counter}{extension}";
                    path = Path.Combine(folderPath, newFilename);
                    counter++;
                }

                fulImg1.SaveAs(path);
                software1.strImagen_sof = newFilename; // Guarda el nombre actualizado en la base de datos
            }
            catch (Exception ex)
            {
                Response.Write("La carga falló: " + ex.Message);
            }
        }

        //bool registro = software1.guardarSoftware();

        //title = registro == true ? "Los datos se han guardado correctamente." : "Los datos no se han guardado correctamente.";
        //icon = registro == true ? "success" : "error";

        string script = $"showAlertAndReload('{title}', '{icon}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

    //public string comprobarRegistro(string resultado)
    //{
    //    string tipoConsulta = "xEstado";
    //    int acum = 0;

    //    while (true)
    //    {
    //        software1.strCod_Fac = resultado;
    //        software1.strCod_Sede = "";
    //        //DataTable tablaDatos = software1.obtenerSoftware(tipoConsulta);

    //        //if (tablaDatos.Rows.Count > 0)
    //        //{
    //        //    ++acum;
    //        //    resultado = resultado + "_" + acum;
    //        //}
    //        //else
    //        //{
    //        //    break;
    //        //}
    //    }

    //    return resultado;
    //}
    //private string generarIdSoft()
    //{
    //    string facultadId = ddlFacultad.SelectedValue;
    //    string sedeId = ddlSede.SelectedValue;
    //    string frase = txtNombre.Text.ToUpper();
    //    string[] palabras = frase.Split(' ');

    //    List<string> partes = new List<string>();

    //    foreach (string palabra in palabras)
    //    {
    //        // Si tiene más de 5 caracteres, tomamos solo los primeros 5
    //        string parte = palabra.Length > 3 ? palabra.Substring(0, 3) : palabra;
    //        partes.Add(parte);
    //    }

    //    string resultado = string.Join("", partes);
    //    resultado = sedeId + "_" + facultadId + "_" + resultado;

    //    string codSoft = comprobarRegistro(resultado);

    //    return codSoft;
    //}

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        content_NombreLicencia.Visible = ddlTipo.SelectedValue == "Propietario" ? true : false;
        content_NombreLicenciaAct.Visible = ddlTipoAct.SelectedValue == "Propietario" ? true : false;
    }

    public void llenarFormActualizar()
    {
        lblIdSoftAct.Text = software1.strCod_sof;
        txtNombreAct.Text = software1.strNombre_sof;
        ddlTipoAct.SelectedValue = software1.strTipoLicencia_sof;

        content_NombreLicenciaAct.Visible = software1.strTipoLicencia_sof == "Propietario" ? true : false;

        txtNombreLicenciaAct.Text = software1.strNombreLicencia_sof;
        txtCantidadAct.Text = software1.intCantidad_sof.ToString();
        txtCostoAct.Text = software1.decCostoUnitario_sof.ToString();
        txtDescripcionAct.Text = software1.strDescripcion_sof;
        txtLinkAct.Text = software1.strUrl_sof;
        lblImg1NameAct.Text = software1.strImagen_sof;
        ddlSedeAct.SelectedValue = software1.strCod_Sede;
        //cargarFacultad();
        ddlFacultadAct.SelectedValue = software1.strCod_Fac;
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        precioUnitario = txtCostoAct.Text != "" ? decimal.Parse(txtCostoAct.Text) : 0;

        software1.strNombre_sof = txtNombreAct.Text;
        software1.strTipoLicencia_sof = ddlTipoAct.SelectedValue;
        software1.strNombreLicencia_sof = txtNombreLicenciaAct.Text;
        software1.intCantidad_sof = int.Parse(txtCantidadAct.Text);
        software1.decCostoUnitario_sof = precioUnitario;
        software1.decCostoTotal_sof = decimal.Parse(txtCantidadAct.Text) * precioUnitario;
        software1.strDescripcion_sof = txtDescripcionAct.Text;
        software1.strUrl_sof = txtLinkAct.Text;
        software1.strCod_sof = lblIdSoftAct.Text;
        software1.dtFecha_log = DateTime.Now;
        software1.strUser_log = Session["Cedula"].ToString();

        if (fulImg1Act.HasFile)
        {
            try
            {
                string folderPath = Server.MapPath("~/images/Software/");
                string filename = Path.GetFileNameWithoutExtension(fulImg1Act.FileName);
                string extension = Path.GetExtension(fulImg1Act.FileName);
                string newFilename = filename + extension;
                string path = Path.Combine(folderPath, newFilename);

                // Verificar si el archivo existe y agregar un sufijo numérico
                int counter = 1;
                while (File.Exists(path))
                {
                    newFilename = $"{filename}_{counter}{extension}";
                    path = Path.Combine(folderPath, newFilename);
                    counter++;
                }

                fulImg1Act.SaveAs(path);
                lblImg1NameAct.Text = newFilename; // Guarda el nombre actualizado en la base de datos
            }
            catch (Exception ex)
            {
                Response.Write("La carga falló: " + ex.Message);
            }
        }
        software1.strImagen_sof = lblImg1NameAct.Text;
        software1.strCod_Fac = ddlFacultadAct.SelectedValue;
        software1.strCod_Sede = ddlSedeAct.SelectedValue;

        //bool actualizar = software1.actualizarSoftware();

        //title = actualizar == true ? "Los datos se han actualizado correctamente." : "Los datos no se han actualizado correctamente.";
        //icon = actualizar == true ? "success" : "error";

        string script = $"showAlertAndReload('{title}', '{icon}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

    protected void gvSoftware_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            software1.strCod_sof = e.CommandArgument.ToString();

            // Carga los detalles del laboratorio según el ID seleccionado   
            //software1.listadoLaboratorioId();

            // Llena el formulario de actualización con los datos cargados
            llenarFormActualizar();

            // Muestra el modal para actualizar los datos
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar').modal('show');", true);
        }
        if (e.CommandName == "Eliminar")
        {
            software1.strCod_sof = e.CommandArgument.ToString();

            // Carga los detalles del laboratorio según el ID seleccionado 
            string comodin = "xCodSof";
            software1.dtFecha_log = DateTime.Now;
            software1.strUser_log = Session["Cedula"].ToString();
            //bool eliminar = software1.eliminarSoftware(comodin);

            //title = eliminar == true ? "Registro eliminado correctamente." : "Registro no eliminado.";
            //icon = eliminar == true ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
    }

    protected void btnVistaCompleta_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string rutaImagen = btn.CommandArgument;
        vistaCompletaImagen.ImageUrl = rutaImagen;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    protected void ddlSede_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarFacultad();
    }
}
