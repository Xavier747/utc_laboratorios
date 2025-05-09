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
using System.Web.Services;

public partial class academic_private_reservaLab_Software : System.Web.UI.Page
{
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    UB_SEDES sede = new UB_SEDES();
    UB_FACULTADES facultad = new UB_FACULTADES();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            cargarSede();
            cargarFacultad();
            cargarSoftware();
            cargarTipoLicencia();
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

    public void cargarTipoLicencia()
    {
        ddlTipo.Items.Add(new ListItem("Propietario", "Propietario"));
        ddlTipo.Items.Add(new ListItem("Libre", "Libre"));

        ddlTipo.Visible = ddlTipo.SelectedValue == "Propietario" ? true : false;
    }

    //guardar nuevo software

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        decimal precioUnitario = txtCosto.Text != "" ? decimal.Parse(txtCosto.Text) : 0;
        string rutaCarpeta = crearDirectorio();

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
        software1.strUser_log = Context.User.Identity.Name;
        software1.bitEstado_sof = true;


        software1.strObs1_sof = string.Empty;
        software1.strObs2_sof = string.Empty;
        software1.bitObs1_sof = false;
        software1.bitObs2_sof = false;
        software1.decObs1_sof = -1;
        software1.decObs2_sof = -1;
        software1.dtObs1_sof = DateTime.Parse("1900-01-01");
        software1.dtObs2_sof = DateTime.Parse("1900-01-01");

        software1.strCod_Fac = ddlFacultad.SelectedValue;
        software1.strCod_Sede = ddlSede.SelectedValue;
        software1.strCod_sof = generarIdSoft();

        

        if (fulImg1.HasFile)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(fulImg1.FileName);
                string extension = Path.GetExtension(fulImg1.FileName);
                string newFilename = filename + extension;
                string path = Path.Combine(rutaCarpeta, newFilename);

                int counter = 1;
                while (File.Exists(path))
                {
                    newFilename = $"{filename}_{counter}{extension}";
                    path = Path.Combine(rutaCarpeta, newFilename);
                    counter++;
                }

                fulImg1.SaveAs(path);
                software1.strImagen_sof = path;
            }
            catch (Exception ex)
            {
                Response.Write("La carga falló: " + ex.Message);
            }
        }

        int registro = software1.AddLAB_SOFTWARE(software1);

        string title = registro != -1 ? software1.msg : software1.msg;
        string icon = registro != -1 ? "success" : "error";
        string ruta = "Software.aspx";

        string script = $"showAlertAndReload('{title}', '{icon}', '{ruta}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

    private string crearDirectorio()
    {
        string rutaCarpeta = "";
        try
        {
            // Ruta que deseas crear
            rutaCarpeta = @"C:\images\Software";

            // Validar si la carpeta ya existe
            if (!Directory.Exists(rutaCarpeta))
            {
                // Crear la carpeta
                Directory.CreateDirectory(rutaCarpeta);
            }

            string rutaParaBD = rutaCarpeta;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocurrió un error al crear la carpeta: " + ex.Message);
        }

        return rutaCarpeta;
    }

    private string generarIdSoft()
    {
        string facultadId = ddlFacultad.SelectedValue;
        string sedeId = ddlSede.SelectedValue;
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
        resultado = sedeId + "_" + facultadId + "_" + resultado;

        string codSoft = comprobarRegistro(resultado);

        return codSoft;
    }

    public string comprobarRegistro(string resultado)
    {
        int acum = 0;

        while (true)
        {
            var tablaDatos = software1.LoadLAB_SOFTWARE("xEstado", resultado, "", "", "");

            if (tablaDatos.Count > 0)
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

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/private/reservalab/NuevoSoftware.aspx");
    }

    protected void gvSoftware_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            Session["codSoft"] = e.CommandArgument.ToString();
            Response.Redirect("~/academic/private/reservalab/ActualizarSoftware.aspx");
        }
        if (e.CommandName == "Eliminar")
        {
            string codSftware = e.CommandArgument.ToString();

            // Carga los detalles del laboratorio según el ID seleccionado 
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string responsable = Context.User.Identity.Name;
            int eliminar = software1.DelLAB_SOFTWARE("xCodSof", codSftware, fecha, responsable, "");

            string title = eliminar != -1 ? "Registro eliminado correctamente." : "Registro no eliminado.";
            string icon = eliminar != -1 ? "success" : "error";

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

    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlSedeAct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {

    }
}
