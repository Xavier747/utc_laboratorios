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

public partial class academic_private_reservalab_MotivoReserva : System.Web.UI.Page
{
    LAB_MOTIVO_RESERVAS motivoReserva = new LAB_MOTIVO_RESERVAS();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            cargarMotivosReserva();
        }
    }

    protected void gvMotivoReserva_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMotivoReserva.PageIndex = e.NewPageIndex;
        cargarMotivosReserva();
    }

    public void cargarMotivosReserva()
    {
        var lista = motivoReserva.LoadLAB_MOTIVO_RESERVAS("ALL", "", "", "", "");

        if (lista != null && lista.Count > 0)
        {
            gvMotivoReserva.DataSource = lista;
            gvMotivoReserva.DataBind();
            lblMsg.Text = motivoReserva.msg;
        }
        else
        {
            lblMsg.Text = motivoReserva.msg;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string codigo = generarIdMotivo(txtNombre.Text.ToUpper());

        motivoReserva.strCod_motRes = codigo;
        motivoReserva.strNombre_motRes = txtNombre.Text.ToUpper();
        motivoReserva.strEstado_motRes = ddlEstado.SelectedValue;
        motivoReserva.dtFechaRegistro_motRes = DateTime.Now;
        motivoReserva.bitEstado_motRes = true;
        motivoReserva.dtFecha_log = DateTime.Now;
        motivoReserva.strUser_log = Context.User.Identity.Name;
        motivoReserva.strObs1_lab = string.Empty;
        motivoReserva.strObs2_lab = string.Empty;
        motivoReserva.bitObs1_lab = false;
        motivoReserva.bitObs2_lab = false;
        motivoReserva.decObs1_lab = -1;
        motivoReserva.decObs2_lab = -1;
        motivoReserva.dtObs1_lab = DateTime.Parse("1900-01-01");
        motivoReserva.dtObs2_lab = DateTime.Parse("1900-01-01");

        motivoReserva.AddLAB_MOTIVO_RESERVAS(motivoReserva);

        string title = motivoReserva.resultado ? motivoReserva.msg : motivoReserva.msg;
        string icon = motivoReserva.resultado ? "success" : "error";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"showAlertAndReload('{title}', '{icon}');", true);
    }

    protected void gvMotivoReserva_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codigo = e.CommandArgument.ToString();

        if (e.CommandName == "Select")
        {
            var motivo = motivoReserva.LoadLAB_MOTIVO_RESERVAS("xPK", codigo, "", "", "");

            lblCodeMotivoAct.Text = motivo[0].strCod_motRes;
            txtNombreAct.Text = motivo[0].strNombre_motRes;

            // 👇 Asignar estado actual al DropDownList de edición
            ddlEstadoAct.SelectedValue = motivo[0].strEstado_motRes;

            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar').modal('show');", true);
        }
        else if (e.CommandName == "Eliminar")
        {
            string dtFecha_log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string strUser_log = Context.User.Identity.Name;
            motivoReserva.DelLAB_MOTIVO_RESERVAS("xCodMotRes", codigo, dtFecha_log, strUser_log, "");

            string title = motivoReserva.resultado ? motivoReserva.msg : motivoReserva.msg;
            string icon = motivoReserva.resultado ? "success" : "error";

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"showAlertAndReload('{title}', '{icon}');", true);
        }
    }

    protected void btn_Actualizar_Click(object sender, EventArgs e)
    {
        motivoReserva.strCod_motRes = lblCodeMotivoAct.Text;
        motivoReserva.strNombre_motRes = txtNombreAct.Text.ToUpper();
        motivoReserva.strEstado_motRes = ddlEstadoAct.SelectedValue; 
        motivoReserva.dtFecha_log = DateTime.Now;
        motivoReserva.strUser_log = Context.User.Identity.Name;

        motivoReserva.UpdateLAB_MOTIVO_RESERVAS(motivoReserva);

        string title = motivoReserva.resultado ? motivoReserva.msg : motivoReserva.msg;
        string icon = motivoReserva.resultado ? "success" : "error";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"showAlertAndReload('{title}', '{icon}');", true);
    }

    private string generarIdMotivo(string nombre)
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
            motivoReserva.strCod_motRes = resultado;
            var lista = motivoReserva.LoadLAB_MOTIVO_RESERVAS(tipoConsulta, resultado, "", "", "");
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
