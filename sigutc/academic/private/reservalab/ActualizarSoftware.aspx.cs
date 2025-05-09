using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;
using System.IO;

public partial class academic_private_reservalab_ActualizarSoftware : System.Web.UI.Page
{
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    UB_SEDES sede = new UB_SEDES();
    UB_FACULTADES facultad = new UB_FACULTADES();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            cargarTipoLicencia();
            cargarSede();            
            consultarRegistro();
        }
    }

    protected void ddlSedeAct_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultadAct.Items.Clear();
    }

    public void cargarTipoLicencia()
    {
        ddlTipoAct.Items.Add(new ListItem("Propietario", "Propietario"));
        ddlTipoAct.Items.Add(new ListItem("Libre", "Libre"));

        ddlTipoAct.Visible = ddlTipoAct.SelectedValue == "Propietario" ? true : false;
    }

    public void cargarSede()
    {
        var listSede = sede.LoadUB_SEDES("ALL", "", "", "", "");
        if (listSede.Count != 0)
        {
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

    public void consultarRegistro()
    {
        string codLab = Session["codSoft"].ToString();
        var registroLab = software1.LoadLAB_SOFTWARE("xPK", codLab, "", "", "");

        ddlSedeAct.SelectedValue = registroLab[0].strCod_Sede;

        cargarFacultad();

        ddlFacultadAct.SelectedValue = registroLab[0].strCod_Fac;
        txtNombreAct.Text = registroLab[0].strNombre_sof;
        txtCantidadAct.Text = registroLab[0].intCantidad_sof.ToString();
        ddlTipoAct.SelectedValue = registroLab[0].strTipoLicencia_sof;

        txtNombreLicenciaAct.Visible = ddlTipoAct.SelectedValue == "Propietario" ? true : false;

        txtNombreLicenciaAct.Text = registroLab[0].strNombreLicencia_sof;
        txtCostoAct.Text = registroLab[0].decCostoUnitario.ToString();
        txtDescripcionAct.Text = registroLab[0].strDescripcion_sof;
        txtLinkAct.Text = registroLab[0].strUrl_sof;
        lblImgActInfo.Text = registroLab[0].strImagen_sof;
    }

    public void cargarFacultad()
    {
        string strCod_Sede = ddlSedeAct.SelectedValue;
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        if (listFacultad.Count != 0)
        {
            ddlFacultadAct.DataSource = listFacultad;
            ddlFacultadAct.DataTextField = "strNombre_fac";
            ddlFacultadAct.DataValueField = "strCod_fac";
            ddlFacultadAct.DataBind();

            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    protected void ddlTipoAct_SelectedIndexChanged(object sender, EventArgs e)
    {
        content_NombreLicenciaAct.Visible = ddlTipoAct.SelectedValue == "Propietario" ? true : false;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/private/reservalab/Software.aspx");
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        string rutaCarpeta = crearDirectorio();
        decimal precioUnitario = txtCostoAct.Text != "" ? decimal.Parse(txtCostoAct.Text) : 0;

        software1.strCod_Fac = ddlFacultadAct.SelectedValue;
        software1.strCod_Sede = ddlSedeAct.SelectedValue;
        software1.strNombre_sof = txtNombreAct.Text;
        software1.strTipoLicencia_sof = ddlTipoAct.SelectedValue;
        software1.strNombreLicencia_sof = txtNombreLicenciaAct.Text;
        software1.intCantidad_sof = int.Parse(txtCantidadAct.Text);
        software1.decCostoUnitario = precioUnitario;
        software1.decCostoTotal = decimal.Parse(txtCantidadAct.Text) * precioUnitario;
        software1.strDescripcion_sof = txtDescripcionAct.Text;
        software1.strUrl_sof = txtLinkAct.Text;
        software1.strCod_sof = Session["codSoft"].ToString();
        software1.dtFecha_log = DateTime.Now;
        software1.strUser_log = Context.User.Identity.Name;

        if (fulImg1Act.HasFile)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(fulImg1Act.FileName);
                string extension = Path.GetExtension(fulImg1Act.FileName);
                string newFilename = filename + extension;
                string path = Path.Combine(rutaCarpeta, newFilename);

                // Verificar si el archivo existe y agregar un sufijo numérico
                int counter = 1;
                while (File.Exists(path))
                {
                    newFilename = $"{filename}_{counter}{extension}";
                    path = Path.Combine(rutaCarpeta, newFilename);
                    counter++;
                }

                fulImg1Act.SaveAs(path);
                lblImgActInfo.Text = path; // Guarda el nombre actualizado en la base de datos
            }
            catch (Exception ex)
            {
                Response.Write("La carga falló: " + ex.Message);
            }
        }

        software1.strImagen_sof = lblImgActInfo.Text;
        software1.UpdateLAB_SOFTWARE(software1);

        string title = software1.resultado ? software1.msg : software1.msg;
        string icon = software1.resultado ? "success" : "error";
        string url = "Software.aspx";

        string script = $"showAlertAndReload('{title}', '{icon}', '{url}');";
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
}