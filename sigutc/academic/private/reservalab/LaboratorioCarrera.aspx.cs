using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryLaboratorios;
using System.Web.Configuration;

using System.Web.UI.WebControls;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;

public partial class academic_private_reservalab_LaboratorioCarrera : System.Web.UI.Page
{
  

    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    LAB_EXCLUSIVO labExc1= new LAB_EXCLUSIVO();
    UB_CARRERAS car = new UB_CARRERAS();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            cargarLaboratorio();
            cargarCarreras();
            cargarCarrerasExclusivas();
        }
    }

   

    public void cargarLaboratorio()
    {
        string strCod_lab = Session["laboratorioId"]?.ToString();

        if (string.IsNullOrEmpty(strCod_lab))
        {
            lblMsg.Text = "No se encontró el ID del laboratorio en la sesión.";
            return;
        }

        var labList = laboratorio2.LoadLAB_LABORATORIOS("xPK", strCod_lab, "", "", "");

        if (labList != null && labList.Count > 0)
        {
            var lab = labList[0];

            lblFacultadId.Text = lab.strCod_Fac;
            lblSedeId.Text = lab.strCod_Sede;
            nombreLboratorio.InnerText = lab.strNombre_lab?.ToUpper();

            lblMsg.Text = laboratorio2.msg;
        }
        else
        {
            lblMsg.Text = "No se encontró información del laboratorio.";
        }
    }

    public void cargarCarreras()
    {
        string tipoConsulta = "xCodLaboratorio";
        string codLab = Session["laboratorioId"].ToString();
        string facultadId = lblFacultadId.Text;
        string sedeId = lblSedeId.Text;

        // Llamada a tu clase de acceso a datos, como haces con sede.LoadUB_SEDES
        var listCarreras = car.LoadUB_CARRERAS(tipoConsulta, facultadId, sedeId, codLab, "");

        if (listCarreras.Count != 0)
        {
            ddlCarreras.Items.Clear();
            ddlCarreras.Items.Add(new ListItem("-- Seleccione una opción --", ""));

            foreach (var item in listCarreras)
            {
                ddlCarreras.Items.Add(new ListItem(item.strnombre_car, item.strcod_car));
            }

            lblMsg.Text = car.msg;
        }
        else
        {
            lblMsg.Text = car.msg;
        }
    }
    private void cargarCarrerasExclusivas()
    {
        var labId = Session["laboratorioId"]?.ToString();
        if (string.IsNullOrEmpty(labId)) return;

        string tipoConsulta = "xCarreraLab";
        var listCarreras = car.LoadUB_CARRERAS(tipoConsulta, labId, "", "", "");

        if (tipoConsulta.Any())
        {
            gvCarreras.DataSource = listCarreras;
            gvCarreras.DataBind();
            lblMsgLstRegistros.Visible = false;
        }
        else
        {
            gvCarreras.DataSource = null;
            gvCarreras.DataBind();
            lblMsgLstRegistros.Visible = true;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string tipoConsulta = "xCarreraLabExc";

        if (string.IsNullOrEmpty(ddlCarreras.SelectedValue))
        {
            string alerta = $"showAlertAndReload('Debe seleccionar una carrera.', 'warning');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", alerta, true);
            return;
        }

        string strCod_Car = ddlCarreras.SelectedValue;

        int validar = validarCarreraUnico(strCod_Car);

        if (validar == 0)
        {
            Random rand = new Random();
            int num = rand.Next(0, 1000);

            string strCod_lab = Session["laboratorioId"].ToString();
            var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", strCod_lab, "", "", "");
            
            // Llenar datos
            labExc1.strCod_lab = listLaboratorio[0].strCod_lab;
            labExc1.strCod_Fac = listLaboratorio[0].strCod_Fac;
            labExc1.strCod_Sede = listLaboratorio[0].strCod_Sede;
            labExc1.strCod_Car = ddlCarreras.SelectedValue;
            labExc1.strCod_labEx = $"{listLaboratorio[0].strCod_Sede}_{listLaboratorio[0].strCod_Fac}_{ddlCarreras.SelectedValue}_{num}";

            labExc1.dtFechaRegistro_labEx = DateTime.Now;
            labExc1.dtFecha_log = DateTime.Now;
            labExc1.strUser_log = Context.User.Identity.Name;
            labExc1.bitEstado_labEx = true;

            // Valores opcionales (pueden ajustarse si es necesario)
            labExc1.strObs1_labEx = "";
            labExc1.strObs2_labEx = "";
            labExc1.bitObs1_labEx = false;
            labExc1.bitObs2_labEx = false;
            labExc1.decObs1_labEx = -1;
            labExc1.decObs2_labEx = -1;
            labExc1.dtObs1_labEx = DateTime.Now;
            labExc1.dtObs2_labEx = DateTime.Now;

            labExc1.AddLAB_EXCLUSIVO(labExc1);

            string title = labExc1.resultado ? labExc1.msg : labExc1.msg;
            string icon = labExc1.resultado ? "success" : "error";

            if (labExc1.resultado)
            {
                cargarCarrerasExclusivas(); // Recarga el GridView
            }

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
        else
        {
            string script = $"showAlertAndReload('La carrera ya se encuentra relacionada con este laboratorio.', 'error');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
    }
    public int validarCarreraUnico(string strCod_Car)
    {
        string tipoConsulta = "xCarreraExclusivo";
        var listCarreras = car.LoadUB_CARRERAS(tipoConsulta, strCod_Car, "", "", "");
        int count = listCarreras.Count;
        return count;
    }

    public string  consultarExclusivo(string codCar)
    {
        var labExclusivo = labExc1.LoadLAB_EXCLUSIVO("xCodCar", codCar, "", "", "");
        return labExclusivo[0].strCod_labEx;
    }


    protected void gvCarreras_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar")
        {
            string codCar = e.CommandArgument.ToString();
            string codLabExc = consultarExclusivo(codCar);

            string strCod_labEx = codLabExc;
            string dtFecha_log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strUser_log = Context.User.Identity.Name;

            labExc1.DelLAB_EXCLUSIVO("xLabExclusivo", strCod_labEx, dtFecha_log, strUser_log,"" );
            string title = labExc1.resultado ? labExc1.msg : labExc1.msg;
            string icon = labExc1.resultado ? "success" : "error";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"showAlertAndReload('{title}','{icon}');", true);
        }
    }
}