using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;
using System.IO;

public partial class academic_private_reservalab_NuevoSoftware : System.Web.UI.Page
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
            cargarFacultad();
        }
    }

    public void cargarSede()
    {
        var listSede = sede.LoadUB_SEDES("ALL", "", "", "", "");
        if (listSede.Count != 0)
        {
            //ddlSede de modal nuevo Software
            ddlSede.DataSource = listSede;
            ddlSede.DataTextField = "strNombre_Sede";
            ddlSede.DataValueField = "strCod_Sede";
            ddlSede.DataBind();

            lblMsg.Text = sede.msg;
        }
        else
        {
            lblMsg.Text = sede.msg;
        }
    }

    protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultad.Items.Clear();
        cargarFacultad();
    }

    public void cargarFacultad()
    {
        string strCod_Sede = ddlSede.SelectedValue;
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        if (listFacultad.Count != 0)
        {
            ddlFacultad.DataSource = listFacultad;
            ddlFacultad.DataTextField = "strNombre_fac";
            ddlFacultad.DataValueField = "strCod_fac";
            ddlFacultad.DataBind();

            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        content_NombreLicencia.Visible = ddlTipo.SelectedValue == "Propietario" ? true : false;
    }

    public void cargarTipoLicencia()
    {
        ddlTipo.Items.Add(new ListItem("Propietario", "Propietario"));
        ddlTipo.Items.Add(new ListItem("Libre", "Libre"));

        ddlTipo.Visible = ddlTipo.SelectedValue == "Propietario" ? true : false;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/private/reservalab/Software.aspx");
    }
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

        string codSoft = generarIdSoft();

        software1.strCod_Fac = ddlFacultad.SelectedValue;
        software1.strCod_Sede = ddlSede.SelectedValue;

        software1.strCod_sof = codSoft;

        if (fulImg1.HasFile)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(fulImg1.FileName);
                string extension = Path.GetExtension(fulImg1.FileName);
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

                fulImg1.SaveAs(path);
                software1.strImagen_sof = path; // Guarda el nombre actualizado en la base de datos
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
}