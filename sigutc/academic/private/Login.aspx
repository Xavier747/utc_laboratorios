<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="academic_private_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>
        Universidad Técnica de Cotopaxi..
    </title>
    <link rel="icon" href="/Styles/Nuevo/assets/images/utc.ico" type="image/x-icon"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <!-- Favicon icon -->
    <link rel="icon" href="/imge/logo.jpeg" type="image/x-icon" />
    
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500" rel="stylesheet" />
    
    <!-- Required Fremwork -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />

    <!-- waves.css -->
    <link rel="stylesheet" href="https://aplicaciones.utc.edu.ec/sigutc/Styles/Nuevo/assets/pages/waves/css/waves.min.css" type="text/css" media="all" />
        
    <!-- Font Awesome -->
    <link rel="stylesheet" type="text/css" href="https://aplicaciones.utc.edu.ec/sigutc/Styles/Nuevo/assets/icon/font-awesome/css/font-awesome.min.css" />

    <!-- Importacion de izitoast -->
    <script src=" https://cdnjs.cloudflare.com/ajax/libs/izitoast/1.4.0/js/iziToast.min.js "></script>
    <link href=" https://cdnjs.cloudflare.com/ajax/libs/izitoast/1.4.0/css/iziToast.min.css " rel="stylesheet" />
    
    <!-- Style.css -->
    <link rel="stylesheet" href="../../Styles/Nuevo/assets/css/Login/Login.css" />
    <link rel="stylesheet" href="../../Styles/Nuevo/assets/css/Login/login_diseño.css" />
</head>
<body themebg-pattern="theme1">
    <section class="login-block">
        <!-- Container-fluid starts -->
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    
                    <!-- Panel para mostrar errores -->
                    <asp:Panel ID="ErrorMessage" runat="server" Visible="false" CssClass="alert alert-danger" >
                        <asp:Label ID="FailureText" runat="server"></asp:Label>
                    </asp:Panel>
                    
                    <!-- Authentication card start -->
                    <form id="form1" runat="server" class="md-float-material form-material">
                        <div class="text-center">
                            <img src="../../Styles/Nuevo/assets/images/logo-blanco-universidad.png" alt="logo.png" style="width: 30%;" />
                        </div>

                        <div class="auth-box card">
                            <div class="card-block">
                                <div class="form-cad-block">
                                    <div class="row m-b-20">
                                        <div class="col-md-12">
                                            <h4 class="text-center">SISTEMA INTEGRADO DE GESTIÓN</h4>
                                            <h5 class="text-center">Iniciar sesión</h5>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group form-primary">
                                        <!-- Se cambia el ID de "txtUsername" a "Usuario" -->
                                        <asp:TextBox ID="Usuario" runat="server" CssClass="form-control"
                                            placeholder="Ingrese Cédula / Pasaporte"></asp:TextBox>
                                        <span class="form-bar"></span>
                                        <asp:Label ID="lblUsername" runat="server" CssClass="float-label-login text-right f-w-600" Text="Usuario"></asp:Label>
                                    </div>
                                    <br />
                                    <div class="form-group form-primary">
                                        <!-- Se cambia el ID de "txtPassword" a "Password" -->
                                        <asp:TextBox ID="Password" TextMode="Password" runat="server" CssClass="form-control"
                                            placeholder="Ingrese la contraseña"></asp:TextBox>
                                        <span class="form-bar"></span>
                                        <asp:Label ID="lblPassword" runat="server" CssClass="float-label-login text-right f-w-600" Text="Contraseña"></asp:Label>
                                    </div>

                                    <div class="row m-t-25 text-left">
                                        <div class="col-12">
                                            <div class="forgot-phone text-right f-right">
                                                <a href="" class="text-right f-w-600" style="text-decoration: none;">¿Has olvidado tu contraseña?</a>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <span id="lblMsg" class="alert-danger"></span>
                                        </div>
                                    </div>

                                    <div class="row m-t-30">
                                        <div class="col-md-12">
                                            

                                            <asp:Button ID="btnLogin" runat="server" Text="INICIO DE SESIÓN"
                                                CssClass="btn btn-primary btn-md btn-block waves-effect waves-light text-center" 
                                                OnClick="LogIn" style="width: 100%;"  />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                    <!-- end of form -->
                </div>
                <!-- end of col-sm-12 -->
            <div aria-live="polite" aria-atomic="true" class="position-fixed top-0 end-0 p-3" style="z-index: 1055;">
                <div id="invalidPasswordToast" class="toast align-items-center border-1" style="background-color:white; border-radius:inherit; border: 5px solid red;" role="alert" aria-live="assertive" aria-atomic="true">
                    <div class="d-flex">
                        <div class="toast-body" style="color: red;">
                            INGRESE CÉDULA / PASAPORTE
                        </div>
                        <button type="button" class="btn-close btn-close-black me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>
            </div>
            <!-- end of row -->
        </div>
        <!-- end of container-fluid -->
    </section>
    <!-- Modal para ingresar la clave de seguridad -->
    <div class="modal fade" id="securityKeyModal" tabindex="-1" aria-labelledby="securityKeyModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="securityKeyModalLabel">Ingresa la clave de seguridad</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="txtSecurityKey">Clave de Seguridad</label>
                        <input type="text" class="form-control" id="txtSecurityKey" placeholder="Ingresa la clave que te enviamos al correo" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" onclick="validateSecurityKey()">Validar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="footer footer-login">
        Desarrollo de software U.T.C
    </div>
</body>
</html>
