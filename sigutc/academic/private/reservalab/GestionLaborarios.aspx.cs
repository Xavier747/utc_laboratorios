using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ClassLibraryLaboratorios;

public partial class academic_private_reservalab_GestionLaborarios : System.Web.UI.Page
{
    //Definicion de las variable de coneccion con la base de datos
    private string cadenaConexion;
    private SqlConnection conexion;

    public LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    private LAB_RESPONSABLE responsable1;
    private LAB_SOFTWARE software1;
    private LAB_LABSOFTWARE softLab;
    private LAB_EXCLUSIVO labExc;
    private LAB_TIPO tipoLaboratorio1;

    private Random rand = new Random();
    public static List<string> softwareSeleccionado;
    public static List<string> nuevosSoftwares;
    public static List<string> softwaresActuales;
    private string tipoConsulta;
    private string filtro;
    private string title;
    private string icon;

    //Metodo principal de la pagina
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //llamado a los metodos que se ejecuta al iniciar la pagina     
            cargarTabla();
            cargarSede();
            cargarTipo();
            cargarCampoAmplio();
        }
    }

    protected void gvLaboratorios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLaboratorios.PageIndex = e.NewPageIndex;
        cargarTabla();
    }

    //redireccion del formulario nuevo laboratorio
    protected void btnNuevoLab_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/private/reservalab/nuevoLaboratorio.aspx");
    }
    protected void gvLaboratorios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            laboratorio2.strCod_Lab = e.CommandArgument.ToString();

            // Carga los detalles del laboratorio según el ID seleccionado   
            //laboratorio2.listarLaboratorioPorId();

            // Llena el formulario de actualización con los datos cargados
            llenarFormActualizar();

            // Muestra el modal para actualizar los datos
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar').modal('show');", true);
        }
        if (e.CommandName == "Eliminar")
        {
            string tipoConsulta = "xPkLab";
            laboratorio2.strCod_Lab = e.CommandArgument.ToString();

            // Carga los detalles del laboratorio según el ID seleccionado   
            //bool eliminar = laboratorio2.eliminarLaboratorio(tipoConsulta);

            //title = eliminar == true ? "Registro eliminado correctamente." : "Registro no eliminado.";
            //icon = eliminar == true ? "success" : "error";

            //string script = $"showAlertAndReload('{title}', '{icon}');";
            //ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
        if (e.CommandName == "Laboratoristas")
        {
            string codLab = e.CommandArgument.ToString();
            tipoConsulta = "xIdLaboratorio";
            obtenerFacultadSede(codLab);
            gestionarResponsable(codLab);
        }
        if (e.CommandName == "Carrera")
        {
            Session["laboratorioId"] = e.CommandArgument.ToString();
            Response.Redirect("~/Views/Laboratoristas/LaboratorioCarrera.aspx");
        }
    }

    public void cargarTabla()
    {
        laboratorio2.strCod_Fac = Session["Permisos"].ToString() == "ADMINISTRADOR" ? "" : Session["Cedula"].ToString();
        tipoConsulta = Session["Permisos"] != null && Convert.ToString(Session["Permisos"]) == "ADMINISTRADOR" ? "ALL" : "xIdPersonal";

        filtro = "";

        //DataTable tablaDatos = laboratorio2.obtenerLaboratorios(tipoConsulta, filtro);
        //try
        //{
        //    if (tablaDatos != null && tablaDatos.Rows.Count > 0) // Verificar si tiene datos
        //    {
        //        gvLaboratorios.DataSource = tablaDatos;
        //        gvLaboratorios.DataBind();
        //    }

        //    lblMsgLstRegistros.Visible = tablaDatos != null && tablaDatos.Rows.Count > 0 ? false : true;
        //}
        //catch (Exception ex)
        //{
        //    // Muestra un error si ocurre
        //    Console.WriteLine("ERROR: " + ex.Message);
        //}
    }

    public void cargarTipo()
    {
        //ddlTipo.Items.Clear();
        ddlTipoAct.Items.Clear();

        //ddlTipo.Items.Add(new ListItem("-- Seleccione una opcion --", ""));
        ddlTipoAct.Items.Add(new ListItem("-- Seleccione una opcion --", ""));

        // Añade cada tipo a los DropdownList
        string tipoConsulta = "ALL";
        tipoLaboratorio1.strCod_tipoLab = "";

        //DataTable tablaDatos = tipoLaboratorio1.consultarTipoLaboratorio(tipoConsulta);

        //foreach (DataRow row in tablaDatos.Rows)
        //{
        //    string codTipo = row["strCod_tipoLab"].ToString();
        //    string nombreTipo = row["strNombre_tipoLab"].ToString();

        //    ddlTipo.Items.Add(new ListItem(nombreTipo, codTipo));
        //    ddlTipoAct.Items.Add(new ListItem(nombreTipo, codTipo));
        //}
    }

    public void cargarCampoAmplio()
    {
        tipoConsulta = "ALL";

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

            //ddlCampoAmplio.Items.Add(new ListItem("-- Seleccione una opcion--", ""));
            ddlCampoAmplioAct.Items.Add(new ListItem("-- Seleccione una opcion--", ""));

            foreach (DataRow row in dt.Rows)
            {
                //ddlCampoAmplio.Items.Add(new ListItem(row["strNombre_areac"].ToString(), row["strCod_areac"].ToString()));
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
        tipoConsulta = "xSedeFacultad";
        //software1.strCod_Fac = ddlFacultad.SelectedValue;
        //software1.strCod_Sede = ddlSede.SelectedValue;

        //DataTable software = software1.obtenerSoftware(tipoConsulta);

        //    listSoftware.Visible = software.Rows.Count > 0 ? true : false;

        //    rptSoftware.DataSource = software;
        //    rptSoftware.DataBind();
    }

    public void cargarSoftwareAct()
    {
        tipoConsulta = "xSedeFacultad";
        software1.strCod_Fac = ddlFacultadAct.SelectedValue;
        software1.strCod_Sede = ddlSedeAct.SelectedValue;

        //DataTable software = software1.obtenerSoftware(tipoConsulta);

        //listSoftwareAct.Visible = software.Rows.Count > 0 ? true : false;

        //rptSoftwareAct.DataSource = software;
        //rptSoftwareAct.DataBind();
    }

    public void cargarSede()
    {
        string idFacultad = "";

        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetUB_SEDES", conexion);
        tipoConsulta = "ALL";

        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", idFacultad);
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

            cargarSoftware();
            cargarSoftwareAct();

            //ddlSede.Items.Add(new ListItem("-- Seleccione una opcion--", ""));
            ddlSedeAct.Items.Add(new ListItem("-- Seleccione una opcion--", ""));
            //ddlFacultad.Items.Add(new ListItem("-- Seleccione una opcion--", ""));

            foreach (DataRow row in dt.Rows)
            {
                //ddlSede.Items.Add(new ListItem(row["strNombre_Sede"].ToString(), row["strCod_Sede"].ToString()));
                ddlSedeAct.Items.Add(new ListItem(row["strNombre_Sede"].ToString(), row["strCod_Sede"].ToString()));
            }
        }
        catch (Exception ex)
        {
            Response.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
    }

    public void cargarFacultad()
    {
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetUB_FACULTADES", conexion);
        tipoConsulta = "xPK";
        string sedeId = "";

        //if (ddlSede.SelectedValue != "") sedeId = ddlSede.SelectedValue;
        //else if (ddlSedeAct.SelectedValue != "") sedeId = ddlSedeAct.SelectedValue;

        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", sedeId);
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

            //ddlFacultad.Items.Add(new ListItem("-- Seleccione una opcion--", ""));
            ddlFacultadAct.Items.Add(new ListItem("-- Seleccione una opcion--", ""));

            foreach (DataRow row in dt.Rows)
            {
                //ddlFacultad.Items.Add(new ListItem(row["strNombre_Fac"].ToString(), row["strCod_Fac"].ToString()));
                ddlFacultadAct.Items.Add(new ListItem(row["strNombre_Fac"].ToString(), row["strCod_Fac"].ToString()));
            }
        }
        catch (Exception ex)
        {
            Response.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
    }

    protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //rptSoftware.DataSource = null;
        //rptSoftware.DataBind();

        //listSoftware.Visible = rptSoftware.DataSource == null ? false : true;

        //ddlFacultad.Items.Clear();
        cargarFacultad();
    }

    protected void ddlSedeAct_SelectedIndexChanged(object sender, EventArgs e)
    {
        //rptSoftware.DataSource = null;
        //rptSoftware.DataBind();

        //listSoftwareAct.Visible = rptSoftware.DataSource == null ? false : true;

        ddlFacultadAct.Items.Clear();
        cargarFacultad();
    }

    protected void ddlFacultad_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarSoftware();
    }

    protected void ddlFacultadAct_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarSoftwareAct();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //string codLab = generarIdLab();
        softwaresActuales = new List<string>();


        //laboratorio2.strCod_Lab = ddlSede.SelectedValue + "_" + ddlFacultad.SelectedValue + "_" + codLab + "_" + DateTime.Now;
        //laboratorio2.strNombre_Lab = txtNombre.Text.ToUpper();
        //laboratorio2.intNumeroEquipos_lab = int.Parse(txtNumeroEquipos.Text);
        //laboratorio2.strUbicacion_Lab = txtUbicacion.Text;
        //laboratorio2.strCod_tipoLab = ddlTipo.SelectedValue;
        //laboratorio2.strCod_areac = ddlCampoAmplio.Text;
        //laboratorio2.dtFechaRegistro_lab = DateTime.Now;
        //laboratorio2.dtFecha_log = DateTime.Now;
        //laboratorio2.strUser_log = Session["Cedula"].ToString();
        //laboratorio2.strCod_Fac = ddlFacultad.SelectedValue;
        //laboratorio2.strCod_Sede = ddlSede.SelectedValue;


        //if (fulImg1.HasFile)
        //{
        //    try
        //    {
        //        string folderPath = Server.MapPath("~/images/Laboratorio/");
        //        string filename = Path.GetFileNameWithoutExtension(fulImg1.FileName);
        //        string extension = Path.GetExtension(fulImg1.FileName);
        //        string newFilename = filename + extension;
        //        string path = Path.Combine(folderPath, newFilename);

        //        // Verificar si el archivo existe y agregar un sufijo numérico
        //        int counter = 1;
        //        while (File.Exists(path))
        //        {
        //            newFilename = $"{filename}_{counter}{extension}";
        //            path = Path.Combine(folderPath, newFilename);
        //            counter++;
        //        }


        //        fulImg1.SaveAs(path);
        //        laboratorio2.strFotografia1_Lab = newFilename;
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("La carga falló: " + ex.Message);
        //    }


        //if (fulImg2.HasFile)
        //{
        //    try
        //    {
        //        string folderPath = Server.MapPath("~/images/Laboratorio/");
        //        string filename = Path.GetFileNameWithoutExtension(fulImg2.FileName);
        //        string extension = Path.GetExtension(fulImg2.FileName);
        //        string newFilename = filename + extension;
        //        string path = Path.Combine(folderPath, newFilename);

        //        // Verificar si el archivo existe y agregar un sufijo numérico
        //        int counter = 1;
        //        while (File.Exists(path))
        //        {
        //            newFilename = $"{filename}_{counter}{extension}";
        //            path = Path.Combine(folderPath, newFilename);
        //            counter++;
        //        }


        //        fulImg2.SaveAs(path);
        //        laboratorio2.strFotografia2_Lab = newFilename;


        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("La carga falló: " + ex.Message);
        //    }
        //}

        //bool registro = laboratorio2.registrarLaboratorio();

        //if (Convert.ToString(Session["Permisos"]) != "ADMINISTRADOR")
        //{
        //    string tipoResp = "Administrativo";
        //    string comodin = "xTipoResponsable";

        //    responsable1.strCod_lab = laboratorio2.strCod_Lab;
        //    responsable1.strCod_res = Session["Cedula"].ToString();
        //    responsable1.strUser_log = Session["Cedula"].ToString();
        //    responsable1.strTipo_respo = "Responsable Administrativo";
        //    responsable1.strCod_res = Session["Cedula"].ToString(); ;
        //    actualizarLaboratorista(comodin, tipoResp);
        //}

        //if (registro)
        //{
        //    relacioanarLaboratorioSoftware();
        //    guardarLaboratorioSoftware();
        //}

        //title = registro == true ? "Los datos se han guardado correctamente." : "Los datos no se han guardado correctamente.";
        //icon = registro == true ? "success" : "error";

        //string script = $"showAlertAndReload('{title}', '{icon}');";
        //ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

    //private string generarIdLab()
    //{
    //    //string frase = txtNombre.Text.ToUpper();
    //    //string[] palabras = frase.Split(' ');

    //    //List<string> partes = new List<string>();

    //    //foreach (string palabra in palabras)
    //    //{
    //    //    // Si tiene más de 5 caracteres, tomamos solo los primeros 5
    //    //    string parte = palabra.Length > 3 ? palabra.Substring(0, 3) : palabra;
    //    //    partes.Add(parte);
    //    //}

    //    //string resultado = string.Join("", partes);
    //    //return resultado;
    //}

    public void relacioanarLaboratorioSoftware()
    {
        softwareSeleccionado = new List<string>();

        //foreach (RepeaterItem item in rptSoftware.Items)
        //{
        //    CheckBox chkSoftware = (CheckBox)item.FindControl("chkSoftware");
        //    if (chkSoftware.Checked)
        //    {
        //        softwareSeleccionado.Add(chkSoftware.ToolTip);
        //    }
        //}
    }

    public void obtenerSoftware()
    {
        softwareSeleccionado = new List<string>();

        foreach (RepeaterItem item in rptSoftwareAct.Items)
        {
            CheckBox chkSoftware = (CheckBox)item.FindControl("chkSoftwareAct");
            if (chkSoftware.Checked)
            {
                softwareSeleccionado.Add(chkSoftware.ToolTip);
            }
        }
    }

    public void guardarLaboratorioSoftware()
    {
        if (nuevosSoftwares != null)
        {
            foreach (string codSoftware in nuevosSoftwares)
            {
                softLab.dtFechaRegistro_labSoft = DateTime.Now;
                softLab.dtFecha_log = DateTime.Now;
                softLab.strUser_log = Session["Cedula"].ToString();
                softLab.strCod_lab = laboratorio2.strCod_Lab;
                softLab.strCod_sof = codSoftware;
                softLab.strCod_Fac = laboratorio2.strCod_Fac;
                softLab.strCod_Sede = laboratorio2.strCod_Sede;
                softLab.strCod_labSoft = laboratorio2.strCod_Lab + "_" + softLab.strCod_sof + "_" + softLab.dtFecha_log;
                softLab.relacionarSoftwareLaboratorio();
            }
        }
    }

    public void llenarFormActualizar()
    {
        // Inicializamos la lista de softwares actuales
        softwaresActuales = new List<string>();

        // Llenar los campos del formulario de acuerdo a las propiedades del objeto laboratorio1
        lblCodeLabAct.Text = laboratorio2.strCod_Lab;
        //txtNombreAct.Text = laboratorio2.strNombre_Lab;
        txtNumeroEquiposAct.Text = laboratorio2.intNumeroEquipos_lab.ToString();
        ddlTipoAct.SelectedValue = laboratorio2.strCod_tipoLab;
        //txtUbicacionAct.Text = laboratorio2.strUbicacion_Lab;
        ddlCampoAmplioAct.SelectedValue = laboratorio2.strCod_areac;
        //lblImg1InfAct.Text = laboratorio2.strFotografia1_Lab;
        //lblImg2InfAct.Text = laboratorio2.strFotografia2_Lab;
        ddlSedeAct.SelectedValue = laboratorio2.strCod_Sede;

        cargarFacultad();

        ddlFacultadAct.SelectedValue = laboratorio2.strCod_Fac;

        cargarSoftwareAct();

        tipoConsulta = "xLaboratorioSoftware";
        softLab.strCod_lab = lblCodeLabAct.Text;
        softLab.strCod_sof = "";
        softLab.strCod_labSoft = "";
        DataTable listaSoftLab = softLab.consultarRelacion(tipoConsulta);
        softwaresActuales = listaSoftLab.AsEnumerable().Select(row => row["strCod_sof"].ToString()).ToList();

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
        //laboratorio2.strNombre_Lab = txtNombreAct.Text;
        laboratorio2.intNumeroEquipos_lab = Convert.ToInt32(txtNumeroEquiposAct.Text);
        //laboratorio2.strUbicacion_Lab = txtUbicacionAct.Text;
        laboratorio2.strCod_tipoLab = ddlTipoAct.SelectedValue;
        laboratorio2.strCod_areac = ddlCampoAmplioAct.SelectedValue;
        laboratorio2.dtFecha_log = DateTime.Now;
        laboratorio2.strUser_log = Session["Cedula"].ToString();
        laboratorio2.strCod_Sede = ddlSedeAct.SelectedValue;
        laboratorio2.strCod_Fac = ddlFacultadAct.SelectedValue;
        laboratorio2.strCod_Lab = lblCodeLabAct.Text;

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
                //laboratorio2.strFotografia1_Lab = newFilename;
            }
            catch (Exception ex)
            {
                Response.Write("La carga de la Imagen 1 falló: " + ex.Message);
            }
        }
        else
        {
            //laboratorio2.strFotografia1_Lab = lblImg1InfAct.Text;
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
                //laboratorio2.strFotografia2_Lab = fulImg2Act.FileName;
            }
            catch (Exception ex)
            {
                Response.Write("La carga de la Imagen 2 falló: " + ex.Message);
            }
        }
        else
        {
            //laboratorio2.strFotografia2_Lab = lblImg2InfAct.Text;
        }

        //bool actualizado = laboratorio2.actualizarLaboratorio();

        obtenerSoftware();
        actualizarSoftware();

        if (Convert.ToString(Session["Permisos"]) != "ADMINISTRADOR")
        {
            string tipoResp = "Administrativo";
            string comodin = "xTipoResponsable";

            responsable1.strCod_res = Session["IdPersonal"].ToString();
            actualizarLaboratorista(comodin, tipoResp);
        }

        //title = actualizado == true ? "Los datos se han actualizado correctamente." : "Los datos no se han actualizado correctamente.";
        //icon = actualizado == true ? "success" : "error";

        //string script = $"showAlertAndReload('{title}', '{icon}');";
        //ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

    public void actualizarSoftware()
    {
        string tipoConsulta = "xLabSoftware";
        nuevosSoftwares = softwareSeleccionado.Except(softwaresActuales).ToList();
        List<string> softwaresEliminados = softwaresActuales.Except(softwareSeleccionado).ToList();

        foreach (string codSoftware in softwaresEliminados)
        {
            softLab.dtFecha_log = DateTime.Now;
            softLab.strUser_log = Session["Cedula"].ToString();
            softLab.strCod_lab = lblCodeLabAct.Text;
            softLab.strCod_sof = codSoftware;
            string idSoftLab = consultarSoftwareLaboratorio();
            if (idSoftLab != "")
            {
                softLab.strCod_labSoft = idSoftLab;
                softLab.eliminarRelacion(tipoConsulta);
            }
        }

        guardarLaboratorioSoftware();
    }

    public string consultarSoftwareLaboratorio()
    {
        tipoConsulta = "xEstadoLabSoft";
        DataTable tablaDatos = softLab.consultarRelacion(tipoConsulta);
        string idSoftLab = tablaDatos.Rows.Count > 0 ? tablaDatos.Rows[0]["strCod_labSoft"].ToString() : "";

        return idSoftLab;
    }

    protected void btnViewImage1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        vistaCompletaImagen.ImageUrl = btn.CommandArgument;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    protected void btnViewImage2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        vistaCompletaImagen.ImageUrl = btn.CommandArgument;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    //Responsables de laboratorio
    private void obtenerFacultadSede(string codLab)
    {
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetUB_SEDES", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", codLab);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", "");
        comandoConsulta.CommandType = CommandType.StoredProcedure;

        try
        {
            conexion.Open();
            SqlDataReader reader = comandoConsulta.ExecuteReader();
            while (reader.Read())
            {
                txtSedeNombre.Text = reader["strNombre_Sede"].ToString();
                txtSedeNuevo.Text = reader["strNombre_Sede"].ToString();
                txtSedeActualizar.Text = reader["strNombre_Sede"].ToString();
                txtFacultadNombre.Text = reader["strNombre_Fac"].ToString();
                txtFacNuevo.Text = reader["strNombre_Fac"].ToString();
                txtFacActualizar.Text = reader["strNombre_Fac"].ToString();
            }
            reader.Close();

        }
        catch (Exception ex)
        {
            Console.Write("ERROR: " + ex.Message);
        }
        conexion.Close();
    }

    protected void gestionarResponsable(string codLab)
    {
        tipoConsulta = "xIdLaboratorio";

        laboratorio2.strCod_Lab = codLab;
        //laboratorio2.detalleLaboratorio();

        lblCodLab.Text = laboratorio2.strCod_Lab;
        //txtLaboratorioNombre.Text = laboratorio2.strNombre_Lab;

        responsable1.strCod_lab = codLab;
        tipoConsulta = "xLaboratorio";

        //List<LAB_RESPONSABLE> responsable = responsable1.detalleResponsableLaboratorio(tipoConsulta);

        //foreach (LAB_RESPONSABLE resp in responsable)
        //{
        //    if (resp.strTipo_respo == "Responsable Academico") txtRespAcad.Text = resp.strObs1_respo;
        //    if (resp.strTipo_respo == "Responsable Administrativo") txtRespAdmin.Text = resp.strObs1_respo;
        //}

        btnAsignarResponsable.Enabled = txtRespAdmin.Text != "" || txtRespAcad.Text != "" ? false : true;
        btnActulizarResponsable.Enabled = txtRespAdmin.Text == "" & txtRespAcad.Text == "" ? false : true;

        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#Lab_Detalle').modal('show');", true);
    }

    protected void btnAsignarResponsable_Click(object sender, EventArgs e)
    {
        tipoConsulta = "xLabExclusivo";
        filtro = lblCodLab.Text;

        laboratorio2.strCod_Lab = lblCodLab.Text;
        //laboratorio2.detalleLaboratorio();

        //txtLabNuevo.Text = laboratorio2.strNombre_Lab;

        //DataTable labs = labExc.obtenerLaboratorios(tipoConsulta, filtro);

        //tipoConsulta = labs.Rows.Count > 0 ? "xResponsable" : "xLaboratorista";
        cargarResponsablesAgregar();

        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#Form_NuevoResponsable').modal('show');", true);
    }

    public void cargarResponsablesAgregar()
    {
        ddlRespAcadNuevo.Items.Clear();
        ddlRespAdminNuevo.Items.Clear();
        ddlRespAcadActualizar.Items.Clear();
        ddlRespAdminActualizar.Items.Clear();

        ddlRespAcadNuevo.Items.Add(new ListItem("-- Seleccione una opcion--", ""));
        ddlRespAdminNuevo.Items.Add(new ListItem("-- Seleccione una opcion--", ""));
        ddlRespAcadActualizar.Items.Add(new ListItem("-- Seleccione una opcion--", ""));
        ddlRespAdminActualizar.Items.Add(new ListItem("-- Seleccione una opcion--", ""));

        consultarPorLabotatorista();
        tipoConsulta = "xDocente";
        consultarPorTipoPersonal();
    }

    private void consultarPorLabotatorista()
    {
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetPERSONAL", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", laboratorio2.strCod_Fac);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", laboratorio2.strCod_Sede);
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
                ddlRespAdminNuevo.Items.Add(new ListItem(row["Responsable"].ToString(), row["CEDULA_ALU"].ToString()));
                ddlRespAdminActualizar.Items.Add(new ListItem(row["Responsable"].ToString(), row["CEDULA_ALU"].ToString()));
            }
        }
        catch (Exception ex)
        {
            Response.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
    }

    private void consultarPorTipoPersonal()
    {
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetPERSONAL", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", laboratorio2.strCod_Fac);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", laboratorio2.strCod_Sede);
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
                ddlRespAcadNuevo.Items.Add(new ListItem(row["Responsable"].ToString(), row["CEDULA_ALU"].ToString()));
                ddlRespAcadActualizar.Items.Add(new ListItem(row["Responsable"].ToString(), row["CEDULA_ALU"].ToString()));
            }
        }
        catch (Exception ex)
        {
            Response.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
    }

    protected void btnGaurdar_Click(object sender, EventArgs e)
    {
        bool guardado = false;
        int acum = 0;
        object[] tipoResponsable = { "Responsable Administrativo", "Responsable Academico" };

        for (int i = 0; i < 2; i++)
        {
            responsable1.strTipo_respo = tipoResponsable[i].ToString();
            responsable1.dtFechaInicio_respo = DateTime.Now;
            responsable1.dtFecha_log = DateTime.Now;
            responsable1.strUser_log = Session["Cedula"].ToString();
            responsable1.strCod_res = i % 2 == 0 ? ddlRespAdminNuevo.SelectedValue : ddlRespAcadNuevo.SelectedValue;
            responsable1.strCod_lab = lblCodLab.Text;
            responsable1.strCod_respo = responsable1.strCod_lab + '_' + responsable1.strCod_res + "_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            //guardado = responsable1.guardarResponsables();

            //if (guardado == true) ++acum;
        }

        title = acum == 2 ? "Los datos se han guardado correctamente." : "Los datos no se han guardado correctamente.";
        icon = acum == 2 ? "success" : "error";

        string script = $"showAlertAndReload('{title}', '{icon}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

    protected void btnActulizarResponsable_Click(object sender, EventArgs e)
    {
        tipoConsulta = "xLabExclusivo";
        filtro = lblCodLab.Text;
        laboratorio2.strCod_Lab = filtro;
        //laboratorio2.detalleLaboratorio();

        //txtLabActualizar.Text = laboratorio2.strNombre_Lab;

        //DataTable labs = labExc.obtenerLaboratorios(tipoConsulta, filtro);

        //tipoConsulta = labs.Rows.Count > 0 ? "xResponsable" : "xLaboratorista";
        cargarResponsablesAgregar();

        responsable1.strCod_lab = filtro;
        tipoConsulta = "xLaboratorio";

        //List<LAB_RESPONSABLE> responsable = responsable1.detalleResponsableLaboratorio(tipoConsulta);

        //foreach (LAB_RESPONSABLE resp in responsable)
        //{
        //    if (resp.strTipo_respo == "Responsable Academico")
        //    {
        //        lblCedulaRespAcad.Text = resp.strCod_res;
        //        ddlRespAcadActualizar.SelectedValue = resp.strCod_res;
        //        lblIdRespAcad.Text = resp.strCod_respo;

        //    }
        //    if (resp.strTipo_respo == "Responsable Administrativo")
        //    {
        //        lblCedulaRespAdmin.Text = resp.strCod_res;
        //        ddlRespAdminActualizar.SelectedValue = resp.strCod_res;
        //        lblIdRespAdmin.Text = resp.strCod_respo;

        //    }
        //}

        //ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#Form_ActualizarResponsable').modal('show');", true);
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        string tipoResp = "";
        DateTime fecha = DateTime.Now;

        if (lblCedulaRespAdmin.Text != ddlRespAdminActualizar.SelectedValue && lblCedulaRespAcad.Text != ddlRespAcadActualizar.SelectedValue)
        {
            object[] codResp = { lblIdRespAdmin.Text, lblIdRespAcad.Text };
            tipoConsulta = "xFKLaboratorio";
            responsable1.dtFecha_log = fecha;
            responsable1.strUser_log = Session["Cedula"].ToString();

            for (int i = 0; i < 2; i++)
            {
                responsable1.strCod_respo = codResp[i].ToString();
                responsable1.strTipo_respo = "";
                //responsable1.actualizarResponsables(tipoConsulta);
            }

            responsable1.strCod_lab = lblCodLab.Text;
            actualizarLaboratorista(tipoConsulta, tipoResp);
        }
        else if (lblCedulaRespAdmin.Text == ddlRespAdminActualizar.SelectedValue && lblCedulaRespAcad.Text != ddlRespAcadActualizar.SelectedValue)
        {
            tipoConsulta = "xTipoResponsable";
            tipoResp = "Academico";

            responsable1.strTipo_respo = "Responsable Academico";
            responsable1.dtFecha_log = DateTime.Now;
            responsable1.strUser_log = Session["Cedula"].ToString();
            responsable1.strCod_respo = lblIdRespAcad.Text;
            //responsable1.actualizarResponsables(tipoConsulta);

            responsable1.strCod_lab = lblCodLab.Text;
            actualizarLaboratorista(tipoConsulta, tipoResp);
        }
        else if (lblCedulaRespAdmin.Text != ddlRespAdminActualizar.SelectedValue && lblCedulaRespAcad.Text == ddlRespAcadActualizar.SelectedValue)
        {
            tipoConsulta = "xTipoResponsable";
            tipoResp = "Administrativo";

            responsable1.strTipo_respo = "Responsable Administrativo";
            responsable1.dtFecha_log = DateTime.Now;
            responsable1.strUser_log = Session["Cedula"].ToString();
            responsable1.strCod_respo = lblIdRespAdmin.Text;
            //responsable1.actualizarResponsables(tipoConsulta);

            responsable1.strCod_res = ddlRespAdminActualizar.SelectedValue;
            responsable1.strCod_lab = lblCodLab.Text;
            actualizarLaboratorista(tipoConsulta, tipoResp);
        }
    }

    public void actualizarLaboratorista(string comodin, string tipoResp)
    {
        int count = 0;
        bool guardado = true;

        if (comodin == "xFKLaboratorio")
        {
            object[] tipoResponsable = { "Responsable Administrativo", "Responsable Academico" };

            for (int i = 0; i < 2; i++)
            {
                responsable1.strTipo_respo = tipoResponsable[i].ToString();
                responsable1.dtFechaInicio_respo = DateTime.Now;
                responsable1.dtFecha_log = DateTime.Now;
                responsable1.strCod_res = i % 2 == 0 ? ddlRespAdminActualizar.SelectedValue : ddlRespAcadActualizar.SelectedValue;
                responsable1.strCod_respo = responsable1.strCod_lab + '_' + responsable1.strCod_res + "_" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"); ;

                //guardado = responsable1.guardarResponsables();

                if (guardado == true) ++count;
            }

            title = count == 2 ? "Los datos se han guardado correctamente." : "Los datos no se han guardado correctamente.";
            icon = count == 2 ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
        else if (comodin == "xTipoResponsable" && tipoResp == "Academico")
        {
            responsable1.dtFechaInicio_respo = DateTime.Now;
            responsable1.dtFecha_log = DateTime.Now;
            responsable1.strCod_res = ddlRespAcadActualizar.SelectedValue;
            responsable1.strCod_respo = responsable1.strCod_lab + '_' + responsable1.strCod_res + "_" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"); ;

            //guardado = responsable1.guardarResponsables();

            //title = guardado == true ? "Los datos se han guardado correctamente." : "Los datos no se han guardado correctamente.";
            //icon = guardado == true ? "success" : "error";

            //string script = $"showAlertAndReload('{title}', '{icon}');";
            //ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
        else if (comodin == "xTipoResponsable" && tipoResp == "Administrativo")
        {
            responsable1.dtFechaInicio_respo = DateTime.Now;
            responsable1.dtFecha_log = DateTime.Now;
            responsable1.strCod_respo = responsable1.strCod_lab + '_' + responsable1.strCod_res + "_" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"); ;
            //guardado = responsable1.guardarResponsables();

            title = guardado == true ? "Los datos se han guardado correctamente." : "Los datos no se han guardado correctamente.";
            icon = guardado == true ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
    }
  
}