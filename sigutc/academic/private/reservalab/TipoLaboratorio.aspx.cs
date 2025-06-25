using ClassLibraryLaboratorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using ClassLibraryTesis;    
using System.Data.SqlClient;
using System.Web.Services;

public partial class academic_private_reservalab_TipoLaboratorio : System.Web.UI.Page
{
    private LAB_TIPO tipoLaboratorio1 = new LAB_TIPO();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Valida si el usuario inicio Sesion
        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            cargarTipoLaboratorio();
        }
    }

    protected void gvTipoLaboratorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTipoLaboratorio.PageIndex = e.NewPageIndex;
        cargarTipoLaboratorio();
    }

    public void cargarTipoLaboratorio()
    {
        //Consulta registros a la base de datos a traves de la libreria de clases
        var listaTipos = tipoLaboratorio1.LoadLAB_TIPO("ALL", "", "", "", "");

        //Valida los registros traidos
        if (listaTipos != null && listaTipos.Count > 0)
        {
            //Carga registros a un GridView
            gvTipoLaboratorio.DataSource = listaTipos;
            gvTipoLaboratorio.DataBind();
            lblMsg.Text = tipoLaboratorio1.msg;
        }
        else
        {
            lblMsg.Text = tipoLaboratorio1.msg;
        }
    }

    //Guardar tipo laboratorio en la base de datos
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //LLamado a la funcion para generar PK
        string codTipo = generarIdSoft(txtNombre.Text.ToUpper()).Trim();

        //Llenado de los atributos
        tipoLaboratorio1.strCod_tipoLab = codTipo;
        tipoLaboratorio1.strNombre_tipoLab = txtNombre.Text.ToUpper().Trim();
        tipoLaboratorio1.dtFechaRegistro_tipoLab = DateTime.Now;
        tipoLaboratorio1.bitEstado_tipoLab = true;
        tipoLaboratorio1.dtFecha_log = DateTime.Now;
        tipoLaboratorio1.strUser_log = Context.User.Identity.Name;
        tipoLaboratorio1.strObs1_tipoLab = string.Empty;
        tipoLaboratorio1.strObs2_tipoLab = string.Empty;
        tipoLaboratorio1.bitObs1_tipoLab = false;
        tipoLaboratorio1.bitObs2_tipoLab = false;
        tipoLaboratorio1.decObs1_tipoLab = -1;
        tipoLaboratorio1.decObs2_tipoLab = -1;
        tipoLaboratorio1.dtObs1_tipoLab = DateTime.Parse("1900-01-01");
        tipoLaboratorio1.dtObs2_tipoLab = DateTime.Parse("1900-01-01");

        //Envio de registro a la libreria de clases
        tipoLaboratorio1.AddLAB_TIPO(tipoLaboratorio1);

        //Mensaje de informacion
        string title = tipoLaboratorio1.resultado ? tipoLaboratorio1.msg :
                       tipoLaboratorio1.numerr == 2627 ? tipoLaboratorio1.msg :
                       "Error: " + tipoLaboratorio1.numerr + "!";
        //Definicion del icono del mensaje
        string icon = tipoLaboratorio1.resultado ? "success" : "error";

        //Ejecucion del codigo js
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"mostrarMensageCRUD('{title}', '{icon}');", true);
    }

    //Metodo para generar la PK del registro a partir del nombre
    private string generarIdSoft(string nombre)
    {
        //Separa el texto en palabras
        string[] palabras = nombre.Split(' ');
        List<string> partes = new List<string>();

        //Recorre las parabras y obtiene las tres primeras letras de cada palabra
        foreach (string palabra in palabras)
        {
            if (!string.IsNullOrWhiteSpace(palabra))
            {
                string parte = palabra.Length > 3 ? palabra.Substring(0, 3) : palabra;
                partes.Add(parte.ToUpper());
            }
        }

        string resultado = string.Join("", partes);
        return resultado;
    }

    protected void gvTipoLaboratorio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codigo = e.CommandArgument.ToString();

        //Valida el comman name y ejecuta la consulta
        if (e.CommandName == "Select")
        {
            var tipo = tipoLaboratorio1.LoadLAB_TIPO("xPK", codigo, "", "", "");

            //llenado del formulario
            lblCodeTipoLabAct.Text = tipo[0].strCod_tipoLab;
            txtNombreAct.Text = tipo[0].strNombre_tipoLab;
            ddlEstadoAct.SelectedValue = tipo[0].bitEstado_tipoLab.ToString();

            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar_Tipo').modal('show');", true);
        }
        //Valida el comman name y ejecuta la consulta
        else if (e.CommandName == "Eliminar")
        {
            string dtFecha_log = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            string strUser_log = Context.User.Identity.Name;
            tipoLaboratorio1.DelLAB_TIPO("xCodTipoLab", codigo, dtFecha_log, strUser_log, "");

            string title = tipoLaboratorio1.resultado ? tipoLaboratorio1.msg :
                           tipoLaboratorio1.numerr == 2627 ? tipoLaboratorio1.msg :
                           "Error: " + tipoLaboratorio1.numerr + "!";
            string icon = tipoLaboratorio1.resultado ? "success" : "error";

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"mostrarMensageCRUD('{title}', '{icon}');", true);
        }
    }
    
    //Actulizar el tipo de laboratorio
    protected void btn_Actualizar_Click(object sender, EventArgs e)
    {
        tipoLaboratorio1.strCod_tipoLab = lblCodeTipoLabAct.Text;
        tipoLaboratorio1.strNombre_tipoLab = txtNombreAct.Text.ToUpper().Trim();
        tipoLaboratorio1.bitEstado_tipoLab = ddlEstadoAct.SelectedValue == "1";
        tipoLaboratorio1.dtFecha_log = DateTime.Now;
        tipoLaboratorio1.strUser_log = Context.User.Identity.Name;

        //Actulaiza el registro
        tipoLaboratorio1.UpdateLAB_TIPO(tipoLaboratorio1);

        //Genera el mensaje de informacion
        string title = tipoLaboratorio1.resultado ? tipoLaboratorio1.msg :
                       tipoLaboratorio1.numerr == 2627 ? tipoLaboratorio1.msg :
                       "Error: " + tipoLaboratorio1.numerr + "!";

        string icon = tipoLaboratorio1.resultado ? "success" : "error";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"mostrarMensageCRUD('{title}', '{icon}');", true);
    }
}