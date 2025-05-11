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
    //Llamado al metodo constructor
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    UB_SEDES sede = new UB_SEDES();
    UB_FACULTADES facultad = new UB_FACULTADES();


    protected void Page_Load(object sender, EventArgs e)
    {
        //Comprobar si el usuario se autentifico a treves del login
        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            //Llamado a los metodos para que realicen la respectiva accion
            cargarSede();
            cargarFacultadNuevo();
            cargarFacultadSoft();
            cargarSoftware();
            cargarTipoLicencia();
           
        }
    }

    protected void ddlSedeSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        //limpiamos el ddl antes de agregar sus respectivos items
        ddlFacultadSoft.Items.Clear();
        cargarFacultadSoft();
        cargarSoftware();
    }

    protected void ddlFacultadSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        //limpiamos la tabla de datos antes de enviar a cargar nuevamente
        gvSoftware.DataSource = null;
        cargarSoftware();
    }

    protected void gvSoftware_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Establecemos paginacion
        gvSoftware.PageIndex = e.NewPageIndex;
    }

    public void cargarSede()
    {
        //Conectamos con la libreria de clases
        //Realiza una consulta a la base de datos
        var listSede = sede.LoadUB_SEDES("ALL", "", "", "", "");

        //Comprueba si devolvieron registros
        if (listSede.Count != 0)
        {
            //ddlSede de listar Software
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

        //Comprueba si devolvieron registros
        if (listSede.Count != 0)
        {
            //ddlSede de listar Software
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

        //Comprueba si devolvieron registros
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


    public void cargarFacultadAct()
    {
        //almacenamos la clave primaria de Sede
        string strCod_Sede = ddlSedeAct.SelectedValue;

        //Realizamos una consulta a la base de datos por las facultaqdes
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        if (listFacultad.Count != 0)
        {
            ddlFacultadAct.DataSource = listFacultad;
            ddlFacultadAct.DataTextField = "strNombre_fac";
            ddlFacultadAct.DataValueField = "strCod_fac";
            ddlFacultadAct.DataBind();

            //muestra mensajes devuelto en la consulta
            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    public void cargarFacultadSoft()
    {
        //almacenamos la clave primaria de Sede
        string strCod_Sede = ddlSedeSoft.SelectedValue;

        //Realizamos una consulta a la base de datos por las facultaqdes
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        if (listFacultad.Count != 0)
        {
            ddlFacultadSoft.DataSource = listFacultad;
            ddlFacultadSoft.DataTextField = "strNombre_fac";
            ddlFacultadSoft.DataValueField = "strCod_fac";
            ddlFacultadSoft.DataBind();

            //muestra mensajes devuelto en la consulta
            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    public void cargarFacultadNuevo()
    {
        //almacenamos la clave primaria de Sede
        string strCod_Sede = ddlSede.SelectedValue;

        //Realizamos una consulta a la base de datos por las facultaqdes
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        if (listFacultad.Count != 0)
        {
            ddlFacultad.DataSource = listFacultad;
            ddlFacultad.DataTextField = "strNombre_fac";
            ddlFacultad.DataValueField = "strCod_fac";
            ddlFacultad.DataBind();

            //muestra mensajes devuelto en la consulta
            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    public void cargarSoftware()
    {
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

    //consultar registro
    public void consultarRegistro(string codSoft)
    {
        var registroSoftware = software1.LoadLAB_SOFTWARE("xPK", codSoft, "", "", "");

        ddlSedeAct.SelectedValue = registroSoftware[0].strCod_Sede;

        cargarFacultadAct();
        lblIdSoftAct.Text = registroSoftware[0].strCod_sof;
        ddlFacultadAct.SelectedValue = registroSoftware[0].strCod_Fac;
        txtNombreAct.Text = registroSoftware[0].strNombre_sof;
        txtCantidadAct.Text = registroSoftware[0].intCantidad_sof.ToString();
        ddlTipoAct.SelectedValue = registroSoftware[0].strTipoLicencia_sof;

        content_NombreLicenciaAct.Visible = ddlTipoAct.SelectedValue == "Propietario" ? true : false;

        txtNombreLicenciaAct.Text = registroSoftware[0].strNombreLicencia_sof;
        txtCostoAct.Text = registroSoftware[0].decCostoUnitario_sof.ToString();
        txtDescripcionAct.Text = registroSoftware[0].strDescripcion_sof;
        txtLinkAct.Text = registroSoftware[0].strUrl_sof;
        lblImg1NameAct.Text = registroSoftware[0].strImagen_sof;
    }

    public void cargarTipoLicencia()
    {
        ddlTipo.Items.Add(new ListItem("Propietario", "Propietario"));
        ddlTipo.Items.Add(new ListItem("Libre", "Libre"));

        ddlTipoAct.Items.Add(new ListItem("Propietario", "Propietario"));
        ddlTipoAct.Items.Add(new ListItem("Libre", "Libre"));

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
        //Redirecciona a otro formulario
        Response.Redirect("~/academic/private/reservalab/NuevoSoftware.aspx");
    }

    protected void gvSoftware_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Definimos dos condicion con una opciones de acuerdo al nombre del Comando
        if (e.CommandName == "Select")

        {
            //capturamos el id para transportar a otro formulario
            string codSoft = e.CommandArgument.ToString();
            consultarRegistro(codSoft);
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar').modal('show');", true);

        }
        if (e.CommandName == "Eliminar")
        {
            string codSftware = e.CommandArgument.ToString();

            // Carga los detalles del laboratorio según el ID seleccionado 
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string responsable = Context.User.Identity.Name;
            software1.DelLAB_SOFTWARE("xCodSof", codSftware, fecha, responsable, "");

            //Muestra mensajes de acuerdo a la respuesta del servidor
            string title = software1.resultado ? software1.msg : software1.msg;
            string icon = software1.resultado ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
    }

    protected void btnVistaCompleta_Click(object sender, EventArgs e)
    {
        // Se obtiene el botón que disparó el evento.
        Button btn = (Button)sender;

        // Se extrae la ruta de la imagen desde el atributo CommandArgument del botón.
        string rutaImagen = btn.CommandArgument;

        // Se asigna la ruta obtenida como la URL de la imagen que se mostrará en el visor (Image control).
        vistaCompletaImagen.ImageUrl = rutaImagen;

        // Se utiliza ScriptManager para ejecutar un script de JavaScript que abre un modal (ventana emergente) con jQuery.
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    protected void ddlSede_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultad.Items.Clear();
        cargarFacultadNuevo();
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        content_NombreLicencia.Visible = ddlTipo.SelectedValue == "Propietario" ? true : false;
    }

    protected void ddlSedeAct_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultadAct.Items.Clear();

        cargarFacultadAct();
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        string rutaCarpeta = crearDirectorio();
        decimal precioUnitario = txtCosto.Text != "" ? decimal.Parse(txtCosto.Text) : 0;

        software1.strCod_sof = lblIdSoftAct.Text;
        software1.strNombre_sof = txtNombreAct.Text;
        software1.strTipoLicencia_sof = ddlTipoAct.SelectedValue;
        software1.strNombreLicencia_sof = txtNombreLicenciaAct.Text;
        software1.intCantidad_sof = Convert.ToInt32(txtCantidadAct.Text);
        software1.decCostoUnitario_sof = precioUnitario;
        software1.decCostoTotal_sof = decimal.Parse(txtCantidadAct.Text) * precioUnitario;
        software1.strDescripcion_sof = txtDescripcionAct.Text;
        software1.strUrl_sof = txtLinkAct.Text;
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

                int counter = 1;
                while (File.Exists(path))
                {
                    newFilename = $"{filename}_{counter}{extension}";
                    path = Path.Combine(rutaCarpeta, newFilename);
                    counter++;
                }

                fulImg1Act.SaveAs(path);
                lblImg1NameAct.Text = path;
            }
            catch (Exception ex)
            {
                Response.Write("La carga falló: " + ex.Message);
            }
        }
        software1.strImagen_sof = lblImg1NameAct.Text;

        software1.strCod_Fac = ddlFacultadAct.SelectedValue;
        software1.strCod_Sede = ddlSedeAct.SelectedValue;

        software1.UpdateLAB_SOFTWARE(software1);

        string title = software1.resultado ? software1.msg : software1.msg;
        string icon = software1.resultado ? "success" : "error";

        string script = $"showAlertAndReload('{title}', '{icon}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);


    }

    protected void ddlTipoAct_SelectedIndexChanged(object sender, EventArgs e)
    {
        content_NombreLicenciaAct.Visible = ddlTipoAct.SelectedValue == "Propietario" ? true : false;
    }
}
