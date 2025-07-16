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

    //Genera paginacion a la tabla
    protected void gvLaboratorios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLaboratorios.PageIndex = e.NewPageIndex;
        cargarTabla();
    }

    //Fila de comandos para realixzar una axion segun las condiciones
    protected void gvLaboratorios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codLab = e.CommandArgument.ToString();

        if (e.CommandName == "Select")
        {
            // Llena el formulario de actualización con los datos cargados
            llenarFormActualizar(codLab);

            // Muestra el modal para actualizar los datos
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar1').modal('show');", true);
        }
        if (e.CommandName == "Eliminar")
        {
            //Carga los detalles del laboratorio según el ID seleccionado
            laboratorio2.DelLAB_LABORATORIOS("xPK", codLab, "", "", "");

            //Generacion de mensajes dependiendo la respuesta de la base de datos
            string title = laboratorio2.resultado ? laboratorio2.msg : 
                           laboratorio2.numerr == 2627 ? laboratorio2.msg :
                           "Error: " + laboratorio2.numerr + "!";
            string icon = laboratorio2.resultado ? "success" : "error";
            string script = string.Concat("mostrarMensageCRUD('", title, "', '", icon, "');");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAlert", script, true);

        }
        if (e.CommandName == "Laboratoristas")
        {
            obtenerFacultadSede(codLab);
            consultarResponsable(codLab);
        }
        if (e.CommandName == "Carrera")
        {
            SeguridadUTC sutc = new SeguridadUTC();
            Response.Redirect("~/academic/private/reservalab/LaboratorioCarrera.aspx?In= " + Server.UrlEncode(sutc.Encripta(codLab)));
        }
    }

    //Consulta lista de registros de laboratorio
    public void cargarTabla()
    {
        //Definir el tipo de consulta dependiendo el rol
        string tipoConsulta = Session["ROL"] != null && Convert.ToString(Session["ROL"]) == "ADMINISTRADOR" ? "ALL" : "xIdPersonal";

        //Asignar el usuario logueado a la variable
        string cedula = Context.User.Identity.Name;

        //Consulta de registros
        var tablaDatos = laboratorio2.LoadLAB_LABORATORIOS(tipoConsulta, cedula, "", "", "");

        /*
            - Comparamos si la cosulta devuelve resultados
            - Caso de ser verdadero muestra los registos, caso contrario un mensage
        */
        try
        {
            if (tablaDatos != null && tablaDatos.Count > 0)
            {
                gvLaboratorios.DataSource = tablaDatos;
                gvLaboratorios.DataBind();
            }

            lblMsg.Text = laboratorio2.msg;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
    }

    public void cargarTipo()
    {
        ddlTipo.Items.Add(new ListItem("DOCENCIA", "DOCENCIA"));
        ddlTipo.Items.Add(new ListItem("DOCENCIA E INVESTIGACIÓN", "DOCENCIA E INVESTIGACIÓN"));

        ddlTipoAct.Items.Add(new ListItem("DOCENCIA", "DOCENCIA"));
        ddlTipoAct.Items.Add(new ListItem("DOCENCIA E INVESTIGACIÓN", "DOCENCIA E INVESTIGACIÓN"));
    }

    //Consutar las areas del conocimiento
    /// <summary>
    /// Cambiar consulta
    /// </summary>
    public void cargarCampoAmplio()
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
    }

    //Consultar software correspodiente a esa facultad y sede
    public void cargarSoftware()
    {
        rptSoftware.DataSource = null;
        rptSoftware.DataBind();

        string strCod_Fac = ddlFacultad.SelectedValue;
        string strCod_Sede = ddlSede.SelectedValue;
        var software = software1.LoadLAB_SOFTWARE("xSedeFacultad", strCod_Fac, strCod_Sede, "", "");

        try
        {
            //Validacion: el componente se muestra siempre y cuando haya registros
            listSoftware.Visible = software.Count > 0 ? true : false;
            lblMsgSoft.Visible = software.Count == 0 ? true : false;

            rptSoftware.DataSource = software;
            rptSoftware.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
    }

    //Consultar software correspodiente a esa facultad y sede
    public void cargarSoftwareAct()
    {
        rptSoftwareAct.DataSource = null;
        rptSoftwareAct.DataBind();

        string strCod_Fac = ddlFacultadAct.SelectedValue;
        string strCod_Sede = ddlSedeAct.SelectedValue;
        var software = software1.LoadLAB_SOFTWARE("xSedeFacultad", strCod_Fac, strCod_Sede, "", "");

        try
        {
            //Validacion: el componente se muestra siempre y cuando haya registros
            listSoftwareAct.Visible = software.Count > 0 ? true : false;
            lblMsgSoftAct.Visible = software.Count == 0 ? true : false;

            rptSoftwareAct.DataSource = software;
            rptSoftwareAct.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
    }

    //Consultar Sedes
    public void cargarSede()
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
        }
    }

    //Consultar Facultad
    public void cargarFacultad()
    {
        ddlFacultad.Items.Clear();

        string strCod_Sede = ddlSede.SelectedValue;
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        try
        {
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
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
    }

    //Consultar Facultad
    public void cargarFacultadAct()
    {
        ddlFacultadAct.Items.Clear();

        string strCod_Sede = ddlSedeAct.SelectedValue;
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        try
        {
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
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
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
        //Generacion de Codigo de laboratorio y carpeta para almacenar imagenes
        string codLab = generarIdLab();
        string rutaCarpeta = crearDirectorio();

        //Llenado de atributos
        laboratorio2.strCod_lab = ddlSede.SelectedValue + "_" + ddlFacultad.SelectedValue + "_" + codLab;
        laboratorio2.strCod_Fac = ddlFacultad.SelectedValue;
        laboratorio2.strCod_Sede = ddlSede.SelectedValue;
        laboratorio2.strNombre_lab = txtNombre.Text.Trim().ToUpper();
        laboratorio2.strDescripcion_lab = txtDescripcion.Text.Trim();
        laboratorio2.intNumeroEquipos_lab = int.Parse(txtNumeroEquipos.Text);
        laboratorio2.strUbicacion_lab = txtUbicacion.Text.Trim();
        laboratorio2.strTipo_lab = ddlTipo.SelectedValue;
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

        //Valida si la imagen existe
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
                    newFilename = string.Concat(filename + "_" + counter + extension);
                    path = Path.Combine(rutaCarpeta, newFilename);
                    counter++;
                }

                //Guarda imagen la ruta definida
                fulImg1.SaveAs(path);
                laboratorio2.strFotografia1_lab = path;
            }
            catch (Exception ex)
            {
                Response.Write("La carga falló: " + ex.Message);
            }
        }

        //Valida si la imagen existe
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
                    newFilename = string.Concat(filename + "_" + counter + extension);
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
        
        //Guardar datos en la base de datos
        laboratorio2.AddLAB_LABORATORIOS(laboratorio2);

        //Valida si el usuario es administrador
        //Si el usario es Laboratorista crea un registro donde se le asigan un laboratorio
        if (Session["ROL"].ToString() != "ADMINISTRADOR")
        {
            responsable1.strCod_lab = laboratorio2.strCod_lab;
            responsable1.strCod_res = Context.User.Identity.Name;
            responsable1.strTipo_respo = "Responsable Administrativo";

            guardarResponsable();
        }

        //Si el laboratorio se guardo con exito tambien puedo guardar software correspondiente al laboratorio
        if (laboratorio2.resultado)
        {
            relacioanarLaboratorioSoftware();
            guardarLaboratorioSoftware();
        }

        //Muestra mensaje de informacion
        string title = laboratorio2.resultado ? laboratorio2.msg :
                       laboratorio2.numerr == 2627 ? laboratorio2.msg :
                       "Error: " + laboratorio2.numerr + "!";
        string icon = laboratorio2.resultado ? "success" : "error";
        string script = string.Concat("mostrarMensageCRUD('", title, "', '", icon, "');");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAlert", script, true);
    }

    //Crea una carpeta para almacenar localmente las imagenes
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

    //Apartir del nombre se genera una clave para codigo de laboratorio
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

    //Obtener todos los software seleccionados en el formulario Nuevo
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

    //Obtener todos los nuevos software seleccionados en el formulario Actulizar
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

    //Guardar la relacion entre software y laboratorio
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

        try
        {
            // Inicializamos la lista de softwares actuales
            softwaresActuales = new List<string>();

            // Llenar los campos del formulario de acuerdo a las propiedades del objeto laboratorio1
            lblCodeLabAct.Text = listLaboratoiros[0].strCod_lab;
            txtNombreAct.Text = listLaboratoiros[0].strNombre_lab;
            txtDescripcionAct.Text = listLaboratoiros[0].strDescripcion_lab;
            txtNumeroEquiposAct.Text = listLaboratoiros[0].intNumeroEquipos_lab.ToString();
            ddlTipoAct.SelectedValue = listLaboratoiros[0].strTipo_lab;
            txtUbicacionAct.Text = listLaboratoiros[0].strUbicacion_lab;
            ddlCampoAmplioAct.SelectedValue = listLaboratoiros[0].strCod_areac;
            lblImg1InfAct.Text = listLaboratoiros[0].strFotografia1_lab;
            lblImg2InfAct.Text = listLaboratoiros[0].strFotografia2_lab;
            ddlEstadoAct.SelectedValue = listLaboratoiros[0].bitEstado_lab == true ? "1" : "0";
            ddlSedeAct.SelectedValue = listLaboratoiros[0].strCod_Sede;

            //Consultar las facultades
            cargarFacultadAct();
            ddlFacultadAct.SelectedValue = listLaboratoiros[0].strCod_Fac;

            cargarSoftwareAct();

            string strCod_lab = lblCodeLabAct.Text;
            var listaSoftLab = softLab.LoadLAB_LABSOFTWARE("xLaboratorioSoftware", strCod_lab, "", "", "");
            softwaresActuales = listaSoftLab.Select(item => item.strCod_sof).ToList();

            isSelectSoftware();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
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

    //Actulizar Laboratorio
    protected void btn_Actualizar_Click(object sender, EventArgs e)
    {
        string rutaCarpeta = crearDirectorio();

        laboratorio2.strCod_lab = lblCodeLabAct.Text;
        laboratorio2.strTipo_lab = ddlTipoAct.SelectedValue;
        laboratorio2.strCod_areac = ddlCampoAmplioAct.SelectedValue;
        laboratorio2.strNombre_lab = txtNombreAct.Text.ToUpper().Trim();
        laboratorio2.strDescripcion_lab = txtDescripcionAct.Text.Trim();
        laboratorio2.intNumeroEquipos_lab = Convert.ToInt32(txtNumeroEquiposAct.Text);
        laboratorio2.strUbicacion_lab = txtUbicacionAct.Text.Trim();
        laboratorio2.bitEstado_lab = ddlEstadoAct.SelectedValue == "1";
        laboratorio2.dtFecha_log = DateTime.Now;
        laboratorio2.strUser_log = Context.User.Identity.Name;

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
                    newFilename = newFilename = string.Concat(filename + "_" + counter + extension);
                    path = Path.Combine(rutaCarpeta, newFilename);
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
                string filename = Path.GetFileNameWithoutExtension(fulImg2Act.FileName);
                string extension = Path.GetExtension(fulImg2Act.FileName);
                string newFilename = filename + extension;
                string path = Path.Combine(rutaCarpeta, newFilename);

                // Verificar si el archivo existe y agregar un sufijo numérico
                int counter = 1;
                while (File.Exists(path))
                {
                    newFilename = string.Concat(filename + "_" + counter + extension);
                    path = Path.Combine(rutaCarpeta, newFilename);
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
        string script = string.Concat("mostrarMensageCRUD('", title, "', '", icon, "');");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAlert", script, true);
    }

    //Actulizar software de los laboratorios
    public void actualizarSoftware()
    {
        //Obtener sobtware seleccionados 
        softwareSeleccionado = nuevosSoftwares.Except(softwaresActuales).ToList();

        //Descartar software que ya no esta en la lista
        List<string> softwaresEliminados = softwaresActuales.Except(softwareSeleccionado).ToList();

        //Recorrer la lista
        foreach (string codSoftware in softwaresEliminados)
        {
            //Definicion de atributos
            string dtFecha_log = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            string strUser_log = Context.User.Identity.Name;
            string strCod_lab = lblCodeLabAct.Text;
            string strCod_sof = codSoftware;
            string idSoftLab = consultarSoftwareLaboratorio(strCod_lab, strCod_sof);

            if (idSoftLab != "")
            {
                //Definicion de atributos
                softLab.strCod_labSoft = idSoftLab;
                softLab.bitEstado_labSoft = false;
                softLab.dtFecha_log = DateTime.Now;
                softLab.strUser_log = Context.User.Identity.Name;

                //Metodo para actualizar registros
                softLab.UpdateLAB_LABSOFTWARE(softLab);
            }
        }

        //Guardar software pertenecientes al labortatorio en la base de datos
        guardarLaboratorioSoftware();
    }

    //Consultar software relacionados al laboratorio
    public string consultarSoftwareLaboratorio(string strCod_lab, string strCod_sof)
    {
        //Definicion de consulta y la clave primaria de cada registro
        var tablaDatos = softLab.LoadLAB_LABSOFTWARE("xEstadoLabSoft", strCod_lab, strCod_sof, "", "");
        string idSoftLab = tablaDatos.Count > 0 ? tablaDatos[0].strCod_labSoft : "";

        return idSoftLab;
    }

    //Funcion para mostrar la imagen del laboratorio
    protected void btnViewImage1_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        vistaCompletaImagen.ImageUrl = btn.CommandArgument;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    //Funcion para mostrar la imagen del laboratorio
    protected void btnViewImage2_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        vistaCompletaImagen.ImageUrl = btn.CommandArgument;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    //Informacion del laboratorio
    private void obtenerFacultadSede(string codLab)
    {
        var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");

        //Validacion de registros encontrados y asignacion a los componentes
        if (listLaboratorio.Count > 0)
        {
            txtLaboratorioNombre.Text = listLaboratorio[0].strNombre_lab;
            txtFacultadNombre.Text = listLaboratorio[0].strObs2_lab;
            txtSedeNombre.Text = listLaboratorio[0].strObs1_lab;
        }
    }

    protected void consultarResponsable(string codLab)
    {
        txtRespAdmin.Text = "";
        txtRespAcad.Text = "";
        lblCodLab.Text = codLab;

        //Consulta de registro
        var responsable = responsable1.LoadLAB_RESPONSABLE("xLaboratorio", codLab, "", "", "");

        try
        {
            //Llenado de registro
            for (int i = 0; i < responsable.Count; i++)
            {
                string cedula = responsable[i].strCod_res;
                if (responsable[i].strTipo_respo == "Responsable Academico")
                {
                    txtRespAcad.Text = responsable[i].strObs1_respo;
                } 
                else if (responsable[i].strTipo_respo == "Responsable Administrativo")
                {
                    txtRespAdmin.Text = responsable[i].strObs1_respo;
                }
            }

            //Validacion para habilitar o desabilitar un boton
            btnAsignarResponsable.Enabled = txtRespAdmin.Text != "" || txtRespAcad.Text != "" ? false : true;
            btnActulizarResponsable.Enabled = txtRespAdmin.Text == "" & txtRespAcad.Text == "" ? false : true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#Lab_Detalle').modal('show');", true);
    }

    protected void btnAsignarResponsable_Click(object sender, EventArgs e)
    {
        //Definicion de variables
        string codLab = lblCodLab.Text;
        var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");

        try
        {
            //Validacion de registros encontrados y asignacion a los componentes
            if (listLaboratorio.Count > 0){
                txtLabNuevo.Text = listLaboratorio[0].strNombre_lab;
                txtFacNuevo.Text = listLaboratorio[0].strObs2_lab;
                txtSedeNuevo.Text = listLaboratorio[0].strObs1_lab;
            } 

            cargarResponsablesAgregar();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#Form_NuevoResponsable').modal('show');", true);
    }

    public void cargarResponsablesAgregar()
    {
        string codLab = lblCodLab.Text;

        //Limpieaza de los ddl
        ddlRespAcadNuevo.Items.Clear();
        ddlRespAdminNuevo.Items.Clear();

        //Consulta de registros
        var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");
        string codSede = listLaboratorio[0].strCod_Sede;
        string codFacultad = listLaboratorio[0].strCod_Fac;

        try
        {
            if (listLaboratorio.Count > 0){
                txtLabNuevo.Text = listLaboratorio[0].strNombre_lab;
                txtFacNuevo.Text = listLaboratorio[0].strObs2_lab;
                txtSedeNuevo.Text = listLaboratorio[0].strObs1_lab;
            } 
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }

        var listPersonal = personal1.Load_PERSONAL("xLaboratorista", codFacultad, codSede, "", "");

        try
        {
            if (listPersonal.Count > 0)
            {
                var listaConcatenada = listPersonal
                    .Select(labResp => new {
                        CEDULA_ALU = labResp.cedula_alu,

                        // Aquí concatenas lo que necesites
                        NOMBRE_COMPLETO = labResp.apellido_alu + " " + labResp.apellidom_alu + " " + labResp.nombre_alu 
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

                    // Aquí concatenas lo que necesites
                    NOMBRE_COMPLETO = labResp.apellido_alu + " " + labResp.apellidom_alu + " " + labResp.nombre_alu
                    }).ToList();

                ddlRespAcadNuevo.DataSource = listaConcatenada;
                ddlRespAcadNuevo.DataTextField = "NOMBRE_COMPLETO";
                ddlRespAcadNuevo.DataValueField = "CEDULA_ALU";
                ddlRespAcadNuevo.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
    }

    public void cargarResponsablesActualizar()
    {
        string codLab = lblCodLab.Text;

        ddlRespAcadActualizar.Items.Clear();
        ddlRespAdminActualizar.Items.Clear();

        var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");
        string codSede = listLaboratorio[0].strCod_Sede;
        string codFacultad = listLaboratorio[0].strCod_Fac;

        try
        {
            if (listLaboratorio.Count > 0){
                txtLabActualizar.Text = listLaboratorio[0].strNombre_lab;
                txtFacActualizar.Text = listLaboratorio[0].strObs2_lab;
                txtSedeActualizar.Text = listLaboratorio[0].strObs1_lab;
            } 
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }

        var listPersonal = personal1.Load_PERSONAL("xLaboratorista", codFacultad, codSede, "", "");

        try
        {
            if (listPersonal.Count > 0)
            {
                var listaConcatenada = listPersonal
                    .Select(labResp => new {
                        CEDULA_ALU = labResp.cedula_alu,

                        // Aquí concatenas lo que necesites
                        NOMBRE_COMPLETO = labResp.apellido_alu + " " + labResp.apellidom_alu + " " + labResp.nombre_alu 
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
                    
                        // Aquí concatenas lo que necesites
                        NOMBRE_COMPLETO = labResp.apellido_alu + " " + labResp.apellidom_alu + " " + labResp.nombre_alu
                    }).ToList();

                ddlRespAcadActualizar.DataSource = listaConcatenada;
                ddlRespAcadActualizar.DataTextField = "NOMBRE_COMPLETO";
                ddlRespAcadActualizar.DataValueField = "CEDULA_ALU";
                ddlRespAcadActualizar.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }

    }

    protected void btnGaurdar_Click(object sender, EventArgs e)
    {
        //Definir una matriz con los tipos de usuaruos
        object[] tipoResponsable = { "Responsable Administrativo", "Responsable Academico" };

        //guardar un registro
        for (int i = 0; i < 2; i++)
        {
            responsable1.strCod_res = i % 2 == 0 ? ddlRespAdminNuevo.SelectedValue : ddlRespAcadNuevo.SelectedValue;
            responsable1.strTipo_respo = tipoResponsable[i].ToString();

            guardarResponsable();
        }

        //Definicion de mensajes
        string title = responsable1.resultado ? responsable1.msg :
                       responsable1.numerr == 2627 ? responsable1.msg :
                       "Error: " + responsable1.numerr + "!";
        string icon = responsable1.resultado ? "success" : "error";
        string script = string.Concat("mostrarMensageCRUD('", title, "', '", icon, "');");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAlert", script, true);
    }

    public void guardarResponsable()
    {
        //Definir los atributos con vsalores del formulario
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

        //Guardado del registro
        responsable1.AddLAB_RESPONSABLE(responsable1);
    }

    protected void btnActulizarResponsable_Click(object sender, EventArgs e)
    {
        string codLab = lblCodLab.Text;

        //Llamar al metodo de consulta
        cargarResponsablesActualizar();

        //Consultar registros
        var responsable = responsable1.LoadLAB_RESPONSABLE("xLaboratorio", codLab, "", "", "");

        try
        {
            //Seleccion de un responsable seguns su rol
            foreach (LAB_RESPONSABLE resp in responsable)
            {
                if (resp.strTipo_respo == "Responsable Academico")
                {
                    lblCedulaRespAcad.Text = resp.strCod_res;
                    ddlRespAcadActualizar.SelectedValue = resp.strCod_res;
                    lblInfoRespAcad.Text = resp.strCod_respo;

                }

                if (resp.strTipo_respo == "Responsable Administrativo")
                {
                    lblCedulaRespAdmin.Text = resp.strCod_res;
                    ddlRespAdminActualizar.SelectedValue = resp.strCod_res;
                    lblInfoRespAdmin.Text = resp.strCod_respo;
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }

        //Muestra el formulario actualizar
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#Form_ActualizarResponsable').modal('show');", true);
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        //Combrobar si hubo cambios de responsoble
        if (lblCedulaRespAdmin.Text != ddlRespAdminActualizar.SelectedValue && lblCedulaRespAcad.Text != ddlRespAcadActualizar.SelectedValue)
        {
            //Definicion de roles 
            object[] tipoResponsable = { "Responsable Administrativo", "Responsable Academico" };

            //Responsables anteriores
            object[] codResp = { lblInfoRespAdmin.Text, lblInfoRespAcad.Text };

            for (int i = 0; i < 2; i++)
            {
                //Actulaizacion de registro o cambio de estado
                responsable1.strCod_respo = codResp[i].ToString();
                responsable1.bitEstado_respo = false;
                responsable1.dtFecha_log = DateTime.Now;
                responsable1.strUser_log = Context.User.Identity.Name;

                responsable1.UpdateLAB_RESPONSABLE(responsable1);
            }

            for (int i = 0; i < 2; i++)
            {
                //Guardado del nuevo registro
                responsable1.strCod_lab = lblCodLab.Text;
                responsable1.strCod_res = i % 2 == 0 ? ddlRespAdminActualizar.SelectedValue : ddlRespAcadActualizar.SelectedValue;
                responsable1.strTipo_respo = tipoResponsable[i].ToString();

                guardarResponsable();
            }
        }

        //Cambio de responsable administrativo
        else if (lblCedulaRespAdmin.Text == ddlRespAdminActualizar.SelectedValue && lblCedulaRespAcad.Text != ddlRespAcadActualizar.SelectedValue)
        {
            responsable1.strCod_respo = lblInfoRespAcad.Text;
            responsable1.bitEstado_respo = false;
            responsable1.dtFecha_log = DateTime.Now;
            responsable1.strUser_log = Context.User.Identity.Name;

            responsable1.UpdateLAB_RESPONSABLE(responsable1);

            responsable1.strCod_lab = lblCodLab.Text;
            responsable1.strCod_res = ddlRespAcadActualizar.SelectedValue;
            responsable1.strTipo_respo = "Responsable Academico";

            guardarResponsable();
        }

        //Cambio de responsable academico
        else if (lblCedulaRespAdmin.Text != ddlRespAdminActualizar.SelectedValue && lblCedulaRespAcad.Text == ddlRespAcadActualizar.SelectedValue)
        {
            responsable1.strCod_respo = lblInfoRespAdmin.Text;
            responsable1.bitEstado_respo = false;
            responsable1.dtFecha_log = DateTime.Now;
            responsable1.strUser_log = Context.User.Identity.Name;

            responsable1.UpdateLAB_RESPONSABLE(responsable1);

            responsable1.strCod_lab = lblCodLab.Text;
            responsable1.strCod_res = ddlRespAdminActualizar.SelectedValue;
            responsable1.strTipo_respo = "Responsable Administrativo";

            guardarResponsable();
        }

        //Mostrar mensajes
        string title = responsable1.resultado ? responsable1.msg :
                       responsable1.numerr == 2627 ? responsable1.msg :
                       "Error: " + responsable1.numerr + "!";
        string icon = responsable1.resultado ? "success" : "error";
        string script = string.Concat("mostrarMensageCRUD('", title, "', '", icon, "');");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAlert", script, true);
    }
}