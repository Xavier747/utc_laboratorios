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
using ClassLibraryTesis;

public partial class academic_private_reservalab_LaboratorioCarrera : System.Web.UI.Page
{    
    //Llamado a las clases de la libreria de clase
    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    LAB_EXCLUSIVO labExc1= new LAB_EXCLUSIVO();
    UB_CARRERAS car = new UB_CARRERAS();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == "") Response.Redirect("~/academic/public/Login.aspx");

        if (!IsPostBack)
        {
            SeguridadUTC sutc = new SeguridadUTC();

            if (Request.QueryString["In"] != null)
            {
                lblCrono.Text = sutc.Desencripta(Request.Params["In"].ToString());

                //Llamado a los metodos que se deben cargar con la pagina
                cargarLaboratorio();
                cargarCarreras();
                cargarCarrerasExclusivas();
            }
            else
            {
                Response.Redirect("~/academic/private/reservalab/GestionLaboratorios.aspx");
            }
        }
    }  

    public void cargarLaboratorio()
    {
        string strCod_lab = lblCrono.Text;

        //Consultar registro
        var labList = laboratorio2.LoadLAB_LABORATORIOS("xPK", strCod_lab, "", "", "");

        try
        {
            //Cargar datos mostrar en el formulario
            if (labList != null && labList.Count > 0)
            {
                var lab = labList[0];

                lblFacultadId.Text = lab.strCod_Fac;
                lblSedeId.Text = lab.strCod_Sede;
                nombreLboratorio.InnerText = lab.strNombre_lab?.ToUpper();
            }
            else
            {
                lblMsg.Text = laboratorio2.msg;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
    }

    //Mostrar carreras relacionado con el laboratorio
    public void cargarCarreras()
    {
        string tipoConsulta = "xCodLaboratorio";
        string codLab = lblCrono.Text;
        string facultadId = lblFacultadId.Text;
        string sedeId = lblSedeId.Text;

        // Llamada a tu clase de acceso a datos, como haces con sede.LoadUB_SEDES
        var listCarreras = car.LoadUB_CARRERAS(tipoConsulta, facultadId, sedeId, codLab, "");

        try
        {
            //Llenar el lista desplegable
            if (listCarreras.Count != 0)
            {
                ddlCarreras.Items.Clear();

                //Carga registros a un GridView
                ddlCarreras.DataSource = listCarreras;
                ddlCarreras.DataTextField = "strnombre_car";
                ddlCarreras.DataValueField = "strCod_Car"; 
                ddlCarreras.DataBind();

                lblMsg.Text = car.msg;
            }
            else
            {
                lblMsg.Text = car.msg;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
    }


    private void cargarCarrerasExclusivas()
    {
        var labId = lblCrono.Text;
        if (string.IsNullOrEmpty(labId)) return;

        string tipoConsulta = "xCarreraLab";
        var listCarreras = car.LoadUB_CARRERAS(tipoConsulta, labId, "", "", "");

        try
        {
            if (tipoConsulta.Any())
            {
                gvCarreras.DataSource = listCarreras;
                gvCarreras.DataBind();
            }
            else
            {
                gvCarreras.DataSource = null;
                gvCarreras.DataBind();
                lblMsg.Text = car.msg;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: ", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string strCod_Car = ddlCarreras.SelectedValue;
        int validar = validarCarreraUnico(strCod_Car);

        if (validar == 0)
        {
            Random rand = new Random();
            int num = rand.Next(0, 1000);

            string strCod_lab = lblCrono.Text;
            var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", strCod_lab, "", "", "");
            
            // Llenar datos
            labExc1.strCod_labEx = $"{listLaboratorio[0].strCod_Sede}_{listLaboratorio[0].strCod_Fac}_{ddlCarreras.SelectedValue}_{num}";
            labExc1.strCod_lab = listLaboratorio[0].strCod_lab;
            labExc1.strCod_Fac = listLaboratorio[0].strCod_Fac;
            labExc1.strCod_Sede = listLaboratorio[0].strCod_Sede;
            labExc1.strCod_Car = ddlCarreras.SelectedValue;
            labExc1.dtFechaRegistro_labEx = DateTime.Now;
            labExc1.dtFecha_log = DateTime.Now;
            labExc1.strUser_log = Context.User.Identity.Name;
            labExc1.bitEstado_labEx = true;
            labExc1.strObs1_labEx = "";
            labExc1.strObs2_labEx = "";
            labExc1.bitObs1_labEx = false;
            labExc1.bitObs2_labEx = false;
            labExc1.decObs1_labEx = -1;
            labExc1.decObs2_labEx = -1;
            labExc1.dtObs1_labEx = DateTime.Now;
            labExc1.dtObs2_labEx = DateTime.Now;

            //Guardar registro
            labExc1.AddLAB_EXCLUSIVO(labExc1);

            //Construccion del mensaje
            string title = labExc1.resultado ? labExc1.msg :
                           labExc1.numerr == 2627 ? labExc1.msg :
                           "Error: " + labExc1.numerr + "!";
            string icon = labExc1.resultado ? "success" : "error";

            string script = $"mostrarMensageCRUD('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
        else
        {
            string script = $"mostrarMensage('La carrera ya se encuentra relacionada con este laboratorio.', 'error');";
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

            labExc1.strCod_labEx = consultarExclusivo(codCar);
            labExc1.bitEstado_labEx = false;
            labExc1.dtFecha_log = DateTime.Now;
            labExc1.strUser_log = Context.User.Identity.Name;

            //Eliminar registro
            labExc1.UpdateLAB_EXCLUSIVO(labExc1);

            //Construccion del mensaje
            string title = labExc1.resultado ? labExc1.msg :
                           labExc1.numerr == 2627 ? labExc1.msg :
                           "Error: " + labExc1.numerr + "!";
            string icon = labExc1.resultado ? "success" : "error";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"mostrarMensageCRUD('{title}','{icon}');", true);
        }
    }
}