using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;
using System.Web.Configuration;

public partial class academic_private_reservalab_GestionLaborarios : System.Web.UI.Page
{
    //Definicion de las variable de coneccion con la base de datos
    SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);

    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    LAB_LABSOFTWARE softLab = new LAB_LABSOFTWARE();
    LAB_EXCLUSIVO labExc = new LAB_EXCLUSIVO();
    LAB_TIPO tipoLaboratorio1 = new LAB_TIPO();
    UB_FACULTADES facultad = new UB_FACULTADES();
    UB_SEDES sede = new UB_SEDES();
    Personal personal1 = new Personal();

    private Random rand = new Random();
    public static List<string> softwareSeleccionado;
    public static List<string> nuevosSoftwares;
    public static List<string> softwaresActuales;

    //Metodo principal de la pagina
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == "") Response.Redirect("~/academic/public/Login.aspx");

        if (!IsPostBack)
        {
            //llamado a los metodos que se ejecuta al iniciar la pagina     
            cargarTabla();
            cargarSede();
            cargarFacultad();
            cargarSoftware();
            cargarTipo();
            cargarCampoAmplio();
        }
    }

    protected void gvLaboratorios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLaboratorios.PageIndex = e.NewPageIndex;
        cargarTabla();
    }

    protected void gvLaboratorios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string codLab = e.CommandArgument.ToString();

            // Llena el formulario de actualización con los datos cargados
            llenarFormActualizar(codLab);

            // Muestra el modal para actualizar los datos
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar1').modal('show');", true);
        }
        if (e.CommandName == "Eliminar")
        {
            string codLab = e.CommandArgument.ToString();

            //Carga los detalles del laboratorio según el ID seleccionado
            laboratorio2.DelLAB_LABORATORIOS("xPK", codLab, "", "", "");

            string title = laboratorio2.resultado ? laboratorio2.msg : 
                           laboratorio2.numerr == 2627 ? laboratorio2.msg :
                           "Error: " + laboratorio2.numerr + "!";
            string icon = laboratorio2.resultado ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
        if (e.CommandName == "Laboratoristas")
        {
            string codLab = e.CommandArgument.ToString();
            obtenerFacultadSede(codLab);
            gestionarResponsable(codLab);
        }
        if (e.CommandName == "Carrera")
        {
            Session["laboratorioId"] = e.CommandArgument.ToString();
            Response.Redirect("LaboratorioCarrera.aspx");
        }
    }

    public void cargarTabla()
    {
        laboratorio2.strCod_Fac = Session["ROL"].ToString() == "ADMINISTRADOR" ? "" : Context.User.Identity.Name;
        string tipoConsulta = Session["ROL"] != null && Convert.ToString(Session["ROL"]) == "ADMINISTRADOR" ? "ALL" : "xIdPersonal";
        string cedula = Context.User.Identity.Name;

        var tablaDatos = laboratorio2.LoadLAB_LABORATORIOS(tipoConsulta, cedula, "", "", "");
        if (tablaDatos != null && tablaDatos.Count > 0)
        {
            gvLaboratorios.DataSource = tablaDatos;
            gvLaboratorios.DataBind();
        }
    }

    public void cargarTipo()
    {
        var tipo = tipoLaboratorio1.LoadLAB_TIPO("ALL", "", "", "", "");

        if (tipo.Count > 0)
        {
            ddlTipo.DataSource = tipo;
            ddlTipo.DataTextField = "strNombre_tipoLab";
            ddlTipo.DataValueField = "strCod_tipoLab";
            ddlTipo.DataBind();
        }

        if (tipo.Count > 0)
        {
            ddlTipoAct.DataSource = tipo;
            ddlTipoAct.DataTextField = "strNombre_tipoLab";
            ddlTipoAct.DataValueField = "strCod_tipoLab";
            ddlTipoAct.DataBind();
        }
    }

    public void cargarCampoAmplio()
    {
        string tipoConsulta = "ALL";

        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetAREAC", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", "");
        comandoConsulta.CommandType = CommandType.StoredProcedure;
        try
        {
            this.conexion.Open();
            SqlDataAdapter adaptadorAlbum = new SqlDataAdapter(comandoConsulta);
            DataTable dt = new DataTable();
            adaptadorAlbum.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                ddlCampoAmplio.Items.Add(new ListItem(row["strNombre_areac"].ToString(), row["strCod_areac"].ToString()));
                ddlCampoAmplioAct.Items.Add(new ListItem(row["strNombre_areac"].ToString(), row["strCod_areac"].ToString()));
            }
        }
        catch (Exception ex)
        {
            Response.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
    }

    public void cargarSoftware()
    {
        rptSoftware.DataSource = null;
        rptSoftware.DataBind();

        string strCod_Fac = ddlFacultad.SelectedValue;
        string strCod_Sede = ddlSede.SelectedValue;
        var software = software1.LoadLAB_SOFTWARE("xSedeFacultad", strCod_Fac, strCod_Sede, "", "");

        listSoftware.Visible = software.Count > 0 ? true : false;

        rptSoftware.DataSource = software;
        rptSoftware.DataBind();
    }

    public void cargarSoftwareAct()
    {
        rptSoftwareAct.DataSource = null;
        rptSoftwareAct.DataBind();

        string strCod_Fac = ddlFacultadAct.SelectedValue;
        string strCod_Sede = ddlSedeAct.SelectedValue;
        var software = software1.LoadLAB_SOFTWARE("xSedeFacultad", strCod_Fac, strCod_Sede, "", "");

        listSoftwareAct.Visible = software.Count > 0 ? true : false;

        rptSoftwareAct.DataSource = software;
        rptSoftwareAct.DataBind();
    }

    public void cargarSede()
    {
        var listSede = sede.LoadUB_SEDES("ALL", "", "", "", "");

        if (listSede.Count > 0)
        {
            ddlSede.DataSource = listSede;
            ddlSede.DataTextField = "strNombre_Sede";
            ddlSede.DataValueField = "strCod_Sede";
            ddlSede.DataBind();
        }

        if (listSede.Count > 0)
        {
            ddlSedeAct.DataSource = listSede;
            ddlSedeAct.DataTextField = "strNombre_Sede";
            ddlSedeAct.DataValueField = "strCod_Sede";
            ddlSedeAct.DataBind();
        }
    }

    public void cargarFacultad()
    {
        ddlFacultad.Items.Clear();

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

    public void cargarFacultadAct()
    {
        ddlFacultadAct.Items.Clear();

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

    protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
    {
        listSoftware.Visible = rptSoftware.DataSource == null ? false : true;

        cargarFacultad();
        cargarSoftware();
    }

    protected void ddlFacultad_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarSoftware();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string codLab = generarIdLab();
        string rutaCarpeta = crearDirectorio();

        laboratorio2.strCod_lab = ddlSede.SelectedValue + "_" + ddlFacultad.SelectedValue + "_" + codLab;
        laboratorio2.strCod_Fac = ddlFacultad.SelectedValue;
        laboratorio2.strCod_Sede = ddlSede.SelectedValue;
        laboratorio2.strNombre_lab = txtNombre.Text.ToUpper();
        laboratorio2.intNumeroEquipos_lab = int.Parse(txtNumeroEquipos.Text);
        laboratorio2.strUbicacion_lab = txtUbicacion.Text;
        laboratorio2.strCod_tipoLab = ddlTipo.SelectedValue;
        laboratorio2.strCod_areac = ddlCampoAmplio.Text;
        laboratorio2.dtFechaRegistro_lab = DateTime.Now;
        laboratorio2.bitEstado_lab = true;
        laboratorio2.dtFecha_log = DateTime.Now;
        laboratorio2.strUser_log = Context.User.Identity.Name;
        laboratorio2.strObs1_lab = string.Empty;
        laboratorio2.strObs2_lab = string.Empty;
        laboratorio2.bitObs1_lab = false;
        laboratorio2.bitObs2_lab = false;
        laboratorio2.decObs1_lab = -1;
        laboratorio2.decObs2_lab = -1;
        laboratorio2.dtObs1_lab = DateTime.Parse("1900 - 01 - 01");
        laboratorio2.dtObs2_lab = DateTime.Parse("1900 - 01 - 01");

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
                laboratorio2.strFotografia1_lab = path;
            }
            catch (Exception ex)
            {
                Response.Write("La carga falló: " + ex.Message);
            }
        }

        if (fulImg2.HasFile)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(fulImg2.FileName);
                string extension = Path.GetExtension(fulImg2.FileName);
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

                fulImg2.SaveAs(path);
                laboratorio2.strFotografia2_lab = path;
            }
            catch (Exception ex)
            {
                Response.Write("La carga falló: " + ex.Message);
            }
        }

        laboratorio2.AddLAB_LABORATORIOS(laboratorio2);

        if (Session["ROL"].ToString() != "ADMINISTRADOR")
        {
            responsable1.strCod_lab = laboratorio2.strCod_lab;
            responsable1.strCod_res = Context.User.Identity.Name;
            responsable1.strTipo_respo = "Responsable Administrativo";

            guardarResponsable();
        }

        if (laboratorio2.resultado)
        {
            relacioanarLaboratorioSoftware();
            guardarLaboratorioSoftware();
        }

        string title = laboratorio2.resultado ? laboratorio2.msg :
                       laboratorio2.numerr == 2627 ? laboratorio2.msg :
                       "Error: " + laboratorio2.numerr + "!";
        string icon = laboratorio2.resultado ? "success" : "error";

        string script = $"showAlertAndReload('{title}', '{icon}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

    private string crearDirectorio()
    {
        string rutaCarpeta = "";
        try
        {
            // Ruta que deseas crear
            rutaCarpeta = @"C:\images\Laboratorios";

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

    private string generarIdLab()
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
        return resultado;
    }

    public void relacioanarLaboratorioSoftware()
    {
        softwareSeleccionado = new List<string>();

        foreach (RepeaterItem item in rptSoftware.Items)
        {
            CheckBox chkSoftware = (CheckBox)item.FindControl("chkSoftware");
            if (chkSoftware.Checked)
            {
                softwareSeleccionado.Add(chkSoftware.ToolTip);
            }
        }
    }

    public void obtenerSoftware()
    {
        nuevosSoftwares = new List<string>();

        foreach (RepeaterItem item in rptSoftwareAct.Items)
        {
            CheckBox chkSoftware = (CheckBox)item.FindControl("chkSoftwareAct");
            if (chkSoftware.Checked)
            {
                nuevosSoftwares.Add(chkSoftware.ToolTip);
            }
        }
    }

    public void guardarLaboratorioSoftware()
    {
        if (softwareSeleccionado != null)
        {
            foreach (string codSoftware in softwareSeleccionado)
            {
                softLab.strCod_Sede = ddlSedeAct.SelectedValue;
                softLab.strCod_Fac = ddlFacultadAct.SelectedValue;
                softLab.strCod_sof = codSoftware;
                softLab.strCod_lab = laboratorio2.strCod_lab;
                softLab.dtFechaRegistro_labSoft = DateTime.Now;
                softLab.bitEstado_labSoft = true;
                softLab.dtFecha_log = DateTime.Now;
                softLab.strUser_log = Context.User.Identity.Name;
                softLab.strObs1_labSoft = string.Empty;
                softLab.strObs2_labSoft = string.Empty;
                softLab.bitObs1_labSoft = false;
                softLab.bitObs2_labSoft = false;
                softLab.decObs1_labSoft = -1;
                softLab.decObs2_labSoft = -1;
                softLab.dtObs1_labSoft = DateTime.Parse("1900-01-01");
                softLab.dtObs2_labSoft = DateTime.Parse("1900-01-01");
                softLab.strCod_labSoft = laboratorio2.strCod_lab + "_" + softLab.strCod_sof + "_" + softLab.dtFecha_log;
                softLab.AddLAB_LABSOFTWARE(softLab);
            }
        }
    }

    public void llenarFormActualizar(string codLab)
    {
        // Carga los detalles del laboratorio según el ID seleccionado   
        var listLaboratoiros = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");

        // Inicializamos la lista de softwares actuales
        softwaresActuales = new List<string>();

        // Llenar los campos del formulario de acuerdo a las propiedades del objeto laboratorio1
        lblCodeLabAct.Text = listLaboratoiros[0].strCod_lab;
        txtNombreAct.Text = listLaboratoiros[0].strNombre_lab;
        txtNumeroEquiposAct.Text = listLaboratoiros[0].intNumeroEquipos_lab.ToString();
        ddlTipoAct.SelectedValue = listLaboratoiros[0].strCod_tipoLab;
        txtUbicacionAct.Text = listLaboratoiros[0].strUbicacion_lab;
        ddlCampoAmplioAct.SelectedValue = listLaboratoiros[0].strCod_areac;
        lblImg1InfAct.Text = listLaboratoiros[0].strFotografia1_lab;
        lblImg2InfAct.Text = listLaboratoiros[0].strFotografia2_lab;
        ddlEstadoAct.SelectedValue = listLaboratoiros[0].bitEstado_lab == true ? "1" : "0";
        ddlSedeAct.SelectedValue = listLaboratoiros[0].strCod_Sede;

        cargarFacultadAct();

        ddlFacultadAct.SelectedValue = listLaboratoiros[0].strCod_Fac;

        cargarSoftwareAct();

        string strCod_lab = lblCodeLabAct.Text;
        var listaSoftLab = softLab.LoadLAB_LABSOFTWARE("xLaboratorioSoftware", strCod_lab, "", "", "");
        softwaresActuales = listaSoftLab.Select(item => item.strCod_sof).ToList();

        isSelectSoftware();
    }

    public void isSelectSoftware()
    {
        foreach (RepeaterItem item in rptSoftwareAct.Items)
        {
            CheckBox chkSoftware = (CheckBox)item.FindControl("chkSoftwareAct");

            if (chkSoftware != null)
            {
                // Marcar el checkbox si el software está en la lista de la BD
                chkSoftware.Checked = softwaresActuales.Contains(chkSoftware.ToolTip);
            }
        }
    }

    protected void btn_Actualizar_Click(object sender, EventArgs e)
    {
        laboratorio2.strCod_lab = lblCodeLabAct.Text;
        laboratorio2.strCod_tipoLab = ddlTipoAct.SelectedValue;
        laboratorio2.strCod_areac = ddlCampoAmplioAct.SelectedValue;
        laboratorio2.strNombre_lab = txtNombreAct.Text;
        laboratorio2.intNumeroEquipos_lab = Convert.ToInt32(txtNumeroEquiposAct.Text);
        laboratorio2.strUbicacion_lab = txtUbicacionAct.Text;
        laboratorio2.bitEstado_lab = ddlEstadoAct.SelectedValue == "1";
        laboratorio2.dtFecha_log = DateTime.Now;
        laboratorio2.strUser_log = Context.User.Identity.Name;

        if (fulImg1Act.HasFile)
        {
            try
            {
                string folderPath = Server.MapPath("~/images/Laboratorio/");
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
                lblImg1InfAct.Text = newFilename;
            }
            catch (Exception ex)
            {
                Response.Write("La carga de la Imagen 1 falló: " + ex.Message);
            }
        }

        if (fulImg2Act.HasFile)
        {
            try
            {
                string folderPath = Server.MapPath("~/images/Laboratorio/");
                string filename = Path.GetFileNameWithoutExtension(fulImg2Act.FileName);
                string extension = Path.GetExtension(fulImg2Act.FileName);
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

                fulImg2Act.SaveAs(path);
                lblImg2InfAct.Text = fulImg2Act.FileName;
            }
            catch (Exception ex)
            {
                Response.Write("La carga de la Imagen 2 falló: " + ex.Message);
            }
        }

        laboratorio2.strFotografia1_lab = lblImg1InfAct.Text;
        laboratorio2.strFotografia2_lab = lblImg2InfAct.Text;

        laboratorio2.UpdateLAB_LABORATORIOS(laboratorio2);

        obtenerSoftware();
        actualizarSoftware();
        string title = laboratorio2.resultado ? laboratorio2.msg :
                       laboratorio2.numerr == 2627 ? laboratorio2.msg :
                       "Error: " + laboratorio2.numerr + "!";
        string icon = laboratorio2.resultado ? "success" : "error";

        string script = $"showAlertAndReload('{title}', '{icon}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

    public void actualizarSoftware()
    {
        softwareSeleccionado = nuevosSoftwares.Except(softwaresActuales).ToList();
        List<string> softwaresEliminados = softwaresActuales.Except(softwareSeleccionado).ToList();

        foreach (string codSoftware in softwaresEliminados)
        {
            string dtFecha_log = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            string strUser_log = Context.User.Identity.Name;
            string strCod_lab = lblCodeLabAct.Text;
            string strCod_sof = codSoftware;
            string idSoftLab = consultarSoftwareLaboratorio(strCod_lab, strCod_sof);

            if (idSoftLab != "")
            {
                softLab.strCod_labSoft = idSoftLab;
                softLab.bitEstado_labSoft = false;
                softLab.dtFecha_log = DateTime.Now;
                softLab.strUser_log = Context.User.Identity.Name;

                softLab.UpdateLAB_LABSOFTWARE(softLab);
            }
        }

        guardarLaboratorioSoftware();
    }

    public string consultarSoftwareLaboratorio(string strCod_lab, string strCod_sof)
    {
        var tablaDatos = softLab.LoadLAB_LABSOFTWARE("xEstadoLabSoft", strCod_lab, strCod_sof, "", "");
        string idSoftLab = tablaDatos.Count > 0 ? tablaDatos[0].strCod_labSoft : "";

        return idSoftLab;
    }

    protected void btnViewImage1_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        vistaCompletaImagen.ImageUrl = btn.CommandArgument;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    protected void btnViewImage2_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        vistaCompletaImagen.ImageUrl = btn.CommandArgument;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    //Responsables de laboratorio
    private void obtenerFacultadSede(string codLab)
    {
        var listSede = sede.LoadUB_SEDES("xIdLaboratorio", codLab, "", "", "");
        var listFacultad = facultad.LoadUB_FACULTADES("xIdLaboratorio", codLab, "", "", "");
        var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");

        if (listSede.Count > 0) txtSedeNombre.Text = listSede[0].strnombre_sede;

        if (listFacultad.Count > 0) txtFacultadNombre.Text = listFacultad[0].strnombre_fac;

        if (listLaboratorio.Count > 0) txtLaboratorioNombre.Text = listLaboratorio[0].strNombre_lab;
    }

    protected void gestionarResponsable(string codLab)
    {
        txtRespAdmin.Text = "";
        txtRespAcad.Text = "";

        lblCodLab.Text = codLab;

        var responsable = responsable1.LoadLAB_RESPONSABLE("xLaboratorio", codLab, "", "", "");

        for(int i = 0; i < responsable.Count; i++)
        {
            string cedula = responsable[i].strCod_res;
            if (responsable[i].strTipo_respo == "Responsable Academico")
            {
                var personal = personal1.Load_PERSONAL("xCEDULA", cedula, "", "", "");
                txtRespAcad.Text = personal[0].apellido_alu + " " + personal[0].apellidom_alu + " " + personal[0].nombre_alu;
            } 
            else if (responsable[i].strTipo_respo == "Responsable Administrativo")
            {
                var personal = personal1.Load_PERSONAL("xCEDULA", cedula, "", "", "");
                txtRespAdmin.Text = personal[0].apellido_alu + " " + personal[0].apellidom_alu + " " + personal[0].nombre_alu;
            }
        }

        btnAsignarResponsable.Enabled = txtRespAdmin.Text != "" || txtRespAcad.Text != "" ? false : true;
        btnActulizarResponsable.Enabled = txtRespAdmin.Text == "" & txtRespAcad.Text == "" ? false : true;

        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#Lab_Detalle').modal('show');", true);
    }

    protected void btnAsignarResponsable_Click(object sender, EventArgs e)
    {
        string codLab = lblCodLab.Text;
        var listSede = sede.LoadUB_SEDES("xIdLaboratorio", codLab, "", "", "");
        var listFacultad = facultad.LoadUB_FACULTADES("xIdLaboratorio", codLab, "", "", "");
        var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");
        var labExclusivo = labExc.LoadLAB_EXCLUSIVO("xLabExclusivo", codLab, "", "", "");
        string tipoConsulta = labExclusivo.Count > 0 ? "xResponsable" : "xLaboratorista";

        if (listSede.Count > 0) txtSedeNuevo.Text = listSede[0].strnombre_sede;

        if (listFacultad.Count > 0) txtFacNuevo.Text = listFacultad[0].strnombre_fac;

        if (listLaboratorio.Count > 0) txtLabNuevo.Text = listLaboratorio[0].strNombre_lab;

        cargarResponsablesAgregar(tipoConsulta);

        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#Form_NuevoResponsable').modal('show');", true);
    }

    public void cargarResponsablesAgregar(string tipoConsulta)
    {
        string codLab = lblCodLab.Text;

        ddlRespAcadNuevo.Items.Clear();
        ddlRespAdminNuevo.Items.Clear();

        var laboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");
        string codSede = laboratorio[0].strCod_Sede;
        string codFacultad = laboratorio[0].strCod_Fac;

        var laboratorista = personal1.Load_PERSONAL(tipoConsulta, codFacultad, codSede, "", "");

        if(laboratorista.Count > 0)
        {
            var listaConcatenada = laboratorista
                .Select(labResp => new {
                    CEDULA_ALU = labResp.cedula_alu,
                    NOMBRE_COMPLETO = labResp.apellido_alu + " " + labResp.apellidom_alu + " " + labResp.nombre_alu // Aquí concatenas lo que necesites
                }).ToList();

            ddlRespAdminNuevo.DataSource = listaConcatenada;
            ddlRespAdminNuevo.DataTextField = "NOMBRE_COMPLETO";
            ddlRespAdminNuevo.DataValueField = "CEDULA_ALU";
            ddlRespAdminNuevo.DataBind();
        }

        var docente = personal1.Load_PERSONAL("xDocente", codFacultad, codSede, "", "");

        if (docente.Count > 0)
        {
            var listaConcatenada = docente
                .Select(labResp => new {
                    CEDULA_ALU = labResp.cedula_alu,
                    NOMBRE_COMPLETO = labResp.apellido_alu + " " + labResp.apellidom_alu + " " + labResp.nombre_alu // Aquí concatenas lo que necesites
                }).ToList();

            ddlRespAcadNuevo.DataSource = listaConcatenada;
            ddlRespAcadNuevo.DataTextField = "NOMBRE_COMPLETO";
            ddlRespAcadNuevo.DataValueField = "CEDULA_ALU";
            ddlRespAcadNuevo.DataBind();
        }
    }

    public void cargarResponsablesActualizar(string tipoConsulta)
    {
        string codLab = lblCodLab.Text;

        ddlRespAcadActualizar.Items.Clear();
        ddlRespAdminActualizar.Items.Clear();

        var laboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");
        string codSede = laboratorio[0].strCod_Sede;
        string codFacultad = laboratorio[0].strCod_Fac;

        var laboratorista = personal1.Load_PERSONAL(tipoConsulta, codFacultad, codSede, "", "");

        if (laboratorista.Count > 0)
        {
            var listaConcatenada = laboratorista
                .Select(labResp => new {
                    CEDULA_ALU = labResp.cedula_alu,
                    NOMBRE_COMPLETO = labResp.apellido_alu + " " + labResp.apellidom_alu + " " + labResp.nombre_alu // Aquí concatenas lo que necesites
                }).ToList();

            ddlRespAdminActualizar.DataSource = listaConcatenada;
            ddlRespAdminActualizar.DataTextField = "NOMBRE_COMPLETO";
            ddlRespAdminActualizar.DataValueField = "CEDULA_ALU";
            ddlRespAdminActualizar.DataBind();
        }

        var docente = personal1.Load_PERSONAL("xDocente", codFacultad, codSede, "", "");

        if (docente.Count > 0)
        {
            var listaConcatenada = docente
                .Select(labResp => new {
                    CEDULA_ALU = labResp.cedula_alu,
                    NOMBRE_COMPLETO = labResp.apellido_alu + " " + labResp.apellidom_alu + " " + labResp.nombre_alu // Aquí concatenas lo que necesites
                }).ToList();

            ddlRespAcadActualizar.DataSource = listaConcatenada;
            ddlRespAcadActualizar.DataTextField = "NOMBRE_COMPLETO";
            ddlRespAcadActualizar.DataValueField = "CEDULA_ALU";
            ddlRespAcadActualizar.DataBind();
        }
    }

    protected void btnGaurdar_Click(object sender, EventArgs e)
    {
        object[] tipoResponsable = { "Responsable Administrativo", "Responsable Academico" };

        for (int i = 0; i < 2; i++)
        {
            responsable1.strCod_res = i % 2 == 0 ? ddlRespAdminNuevo.SelectedValue : ddlRespAcadNuevo.SelectedValue;
            responsable1.strTipo_respo = tipoResponsable[i].ToString();

            guardarResponsable();
        }

        string title = laboratorio2.resultado ? laboratorio2.msg :
                       laboratorio2.numerr == 2627 ? laboratorio2.msg :
                       "Error: " + laboratorio2.numerr + "!";
        string icon = responsable1.resultado ? "success" : "error";

        string script = $"showAlertAndReload('{title}', '{icon}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

    public void guardarResponsable()
    {
        responsable1.strCod_lab = lblCodLab.Text;            
        responsable1.dtFechaInicio_respo = DateTime.Now;
        responsable1.bitEstado_respo = true;
        responsable1.dtFecha_log = DateTime.Now;
        responsable1.strUser_log = Context.User.Identity.Name;
        responsable1.strObs1_respo = string.Empty;
        responsable1.strObs2_respo = string.Empty;
        responsable1.bitObs1_respo = false;
        responsable1.bitObs2_respo = false;
        responsable1.decObs1_respo = -1;
        responsable1.decObs2_respo = -1;
        responsable1.dtObs1_respo = DateTime.Parse("1900-01-01");
        responsable1.dtObs2_respo = DateTime.Parse("1900-01-01");
        responsable1.strCod_respo = responsable1.strCod_lab + '_' + responsable1.strCod_res + "_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");

        responsable1.AddLAB_RESPONSABLE(responsable1);
    }

    protected void btnActulizarResponsable_Click(object sender, EventArgs e)
    {
        string codLab = lblCodLab.Text;

        var listSede = sede.LoadUB_SEDES("xIdLaboratorio", codLab, "", "", "");
        var listFacultad = facultad.LoadUB_FACULTADES("xIdLaboratorio", codLab, "", "", "");
        var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");

        if (listSede.Count > 0) txtSedeActualizar.Text = listSede[0].strnombre_sede;

        if (listFacultad.Count > 0) txtFacActualizar.Text = listFacultad[0].strnombre_fac;

        if (listLaboratorio.Count > 0) txtLabActualizar.Text = listLaboratorio[0].strNombre_lab;


        var labExclusivo = labExc.LoadLAB_EXCLUSIVO("xLabExclusivo", codLab, "", "", "");

        string tipoConsulta = labExclusivo.Count > 0 ? "xResponsable" : "xLaboratorista";
        cargarResponsablesActualizar(tipoConsulta);

        var responsable = responsable1.LoadLAB_RESPONSABLE("xLaboratorio", codLab, "", "", "");

        foreach (LAB_RESPONSABLE resp in responsable)
        {
            if (resp.strTipo_respo == "Responsable Academico")
            {
                lblCedulaRespAcad.Text = resp.strCod_res;
                ddlRespAcadActualizar.SelectedValue = resp.strCod_res;
                lblIdRespAcad.Text = resp.strCod_respo;

            }
            if (resp.strTipo_respo == "Responsable Administrativo")
            {
                lblCedulaRespAdmin.Text = resp.strCod_res;
                ddlRespAdminActualizar.SelectedValue = resp.strCod_res;
                lblIdRespAdmin.Text = resp.strCod_respo;
            }
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#Form_ActualizarResponsable').modal('show');", true);
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        if (lblCedulaRespAdmin.Text != ddlRespAdminActualizar.SelectedValue && lblCedulaRespAcad.Text != ddlRespAcadActualizar.SelectedValue)
        {
            object[] tipoResponsable = { "Responsable Administrativo", "Responsable Academico" };
            object[] codResp = { lblIdRespAdmin.Text, lblIdRespAcad.Text };


            for (int i = 0; i < 2; i++)
            {
                responsable1.strCod_respo = codResp[i].ToString();
                responsable1.bitEstado_respo = false;
                responsable1.dtFecha_log = DateTime.Now;
                responsable1.strUser_log = Context.User.Identity.Name;

                responsable1.UpdateLAB_RESPONSABLE(responsable1);

                responsable1.strCod_lab = lblCodLab.Text;
                responsable1.strCod_res = i % 2 == 0 ? ddlRespAdminActualizar.SelectedValue : ddlRespAcadActualizar.SelectedValue;
                responsable1.strTipo_respo = tipoResponsable[i].ToString();

                guardarResponsable();
            }
        }
        else if (lblCedulaRespAdmin.Text == ddlRespAdminActualizar.SelectedValue && lblCedulaRespAcad.Text != ddlRespAcadActualizar.SelectedValue)
        {
            responsable1.strCod_respo = lblIdRespAcad.Text;
            responsable1.bitEstado_respo = false;
            responsable1.dtFecha_log = DateTime.Now;
            responsable1.strUser_log = Context.User.Identity.Name;

            responsable1.UpdateLAB_RESPONSABLE(responsable1);

            responsable1.strCod_lab = lblCodLab.Text;
            responsable1.strCod_res = ddlRespAcadActualizar.SelectedValue;
            responsable1.strTipo_respo = "Responsable Academico";

            guardarResponsable();
        }
        else if (lblCedulaRespAdmin.Text != ddlRespAdminActualizar.SelectedValue && lblCedulaRespAcad.Text == ddlRespAcadActualizar.SelectedValue)
        {
            responsable1.strCod_respo = lblIdRespAcad.Text;
            responsable1.bitEstado_respo = false;
            responsable1.dtFecha_log = DateTime.Now;
            responsable1.strUser_log = Context.User.Identity.Name;

            responsable1.UpdateLAB_RESPONSABLE(responsable1);

            responsable1.strCod_lab = lblCodLab.Text;
            responsable1.strCod_res = ddlRespAdminActualizar.SelectedValue;
            responsable1.strTipo_respo = "Responsable Administrativo";

            guardarResponsable();
        }

        string title = laboratorio2.resultado ? laboratorio2.msg :
                       laboratorio2.numerr == 2627 ? laboratorio2.msg :
                       "Error: " + laboratorio2.numerr + "!";
        string icon = responsable1.resultado ? "success" : "error";

        string script = $"showAlertAndReload('{title}', '{icon}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }
}