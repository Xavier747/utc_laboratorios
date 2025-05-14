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
        var listaTipos = tipoLaboratorio1.LoadLAB_TIPO("ALL", "", "", "", "");

        if (listaTipos != null && listaTipos.Count > 0)
        {
            gvTipoLaboratorio.DataSource = listaTipos;
            gvTipoLaboratorio.DataBind();
            lblMsg.Text = tipoLaboratorio1.msg;
        }
        else
        {
            lblMsg.Text = tipoLaboratorio1.msg;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string codTipo = generarIdSoft(txtNombre.Text.ToUpper());
        tipoLaboratorio1.strCod_tipoLab = codTipo;
        tipoLaboratorio1.strNombre_tipoLab = txtNombre.Text.ToUpper();
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
        tipoLaboratorio1.AddLAB_TIPO(tipoLaboratorio1);

        string title = tipoLaboratorio1.resultado ? tipoLaboratorio1.msg : tipoLaboratorio1.msg;
        string icon = tipoLaboratorio1.resultado ? "success" : "error";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"showAlertAndReload('{title}', '{icon}');", true);
    }

    protected void gvTipoLaboratorio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codigo = e.CommandArgument.ToString();
     
        if (e.CommandName == "Select")
        {            
            var tipo = tipoLaboratorio1.LoadLAB_TIPO("xPK", codigo, "", "", "");

            lblCodeTipoLabAct.Text = tipo[0].strCod_tipoLab;
            txtNombreAct.Text = tipo[0].strNombre_tipoLab;

            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar').modal('show');", true);
        }
        else if (e.CommandName == "Eliminar")
        {
            string dtFecha_log = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            string strUser_log = Context.User.Identity.Name;
            tipoLaboratorio1.DelLAB_TIPO("xCodTipoLab", codigo, dtFecha_log, strUser_log,"");

            string title = tipoLaboratorio1.resultado ? tipoLaboratorio1.msg : tipoLaboratorio1.msg;
            string icon = tipoLaboratorio1.resultado ? "success" : "error";

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"showAlertAndReload('{title}', '{icon}');", true);
        }
    }
    
    protected void btn_Actualizar_Click(object sender, EventArgs e)
    {
        tipoLaboratorio1.strCod_tipoLab = lblCodeTipoLabAct.Text;
        tipoLaboratorio1.strNombre_tipoLab = txtNombreAct.Text.ToUpper();
        tipoLaboratorio1.dtFecha_log = DateTime.Now;
        tipoLaboratorio1.strUser_log = Context.User.Identity.Name;

        tipoLaboratorio1.UpdateLAB_TIPO(tipoLaboratorio1);

        string title = tipoLaboratorio1.resultado ? tipoLaboratorio1.msg : tipoLaboratorio1.msg;
        string icon = tipoLaboratorio1.resultado ? "success" : "error";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"showAlertAndReload('{title}', '{icon}');", true);
    }

    private string generarIdSoft(string nombre)
    {
        string[] palabras = nombre.Split(' ');
        List<string> partes = new List<string>();

        foreach (string palabra in palabras)
        {
            if (!string.IsNullOrWhiteSpace(palabra))
            {
                string parte = palabra.Length > 3 ? palabra.Substring(0, 3) : palabra;
                partes.Add(parte.ToUpper());
            }
        }

        string resultado = string.Join("", partes);
        return comprobarRegistro(resultado);
    }

    public string comprobarRegistro(string resultado)
    {
        string tipoConsulta = "xEstado";
        int acum = 0;

        while (true)
        {
            tipoLaboratorio1.strCod_tipoLab = resultado;
            var lista = tipoLaboratorio1.LoadLAB_TIPO(tipoConsulta, resultado, "", "", "");
            if (lista != null && lista.Count > 0)
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