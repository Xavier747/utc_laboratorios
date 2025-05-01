using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryTesis;
public partial class academic_private_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        UB_SEDES sedes = new UB_SEDES();
        UB_FACULTADES fac = new UB_FACULTADES();
        UB_CARRERAS car = new UB_CARRERAS();
        SIG_PERIODOS per = new SIG_PERIODOS();


        var listSedes = sedes.LoadUB_SEDES("ALL", "", "", "","");
        var listFac = fac.LoadUB_FACULTADES("ALL", "", "", "","");
        var listCar = car.LoadUB_CARRERAS("xCodLaboratorio", "CIYA", "MUTC", "","");
        var peri = fac.LoadUB_FACULTADES("ALL", "", "", "", "");

        //sedes

        if (listSedes.Count != 0)
        {
            gvwPrueba.DataSource = listSedes;
            gvwPrueba.DataBind();
            lblMsg.Text = sedes.msg;
        }
        else
        {
            lblMsg.Text = sedes.msg;
        }

        //Facultades
        
        if (listFac.Count != 0)
        {
            ddlFacultad.DataSource = listFac.ToList();
            ddlFacultad.DataTextField = "strNombre_fac";
            ddlFacultad.DataValueField = "strCod_fac";
            ddlFacultad.DataBind();
          
            lblMsg.Text = sedes.msg;
        }
        else
        {
            lblMsg.Text = sedes.msg;
        }
        //Carreras

        if (listCar.Count != 0)
        {
            GridCarrera.DataSource = listCar;
            GridCarrera.DataBind();
            lblMsg.Text = sedes.msg;
        }
        else
        {
            lblMsg.Text = sedes.msg;
        }
    }
}