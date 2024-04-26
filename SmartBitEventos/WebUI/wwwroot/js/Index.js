function Index() {

    this.service = 'Index';
    this.ctrlActions = new ControlActions();
    this.correo = document.getElementById('txtCorreo');

    

    this.action = function () {
        console.log(document.getElementById('txtCorreo'));

        ////callback response from api                                                            //server response
        //this.ctrlActions.GetToApi(this.service + '/?correo=' + data[0] , callback = (response) => {
        //    //sesion del navegador             //JSON to get data

        //    var usuarioQuery = JSON.stringify(response);
        //    var usuario = JSON.parse(usuarioQuery);

        //    switch (usuario.Estado) {
        //        case 'Bloqueado':
        //            alert('Estimado usuario, se le informa que su cuenta con el correo : ' + usuario.Correo + ' esta bloqueada')
        //            break;
        //        case 'Espera':
        //            alert('Estimado usuario, se le informa que su cuenta esta pendiente de aprovación')
        //            break;
        //        case 'Desactivo':
        //            alert('Estimado usuario, se le informa que su cuenta esta desactivada')
        //            break;
        //        case 'Activo':

        //            sessionStorage.setItem('tipo', usuario.TipoUsuario),
        //                sessionStorage.setItem('id', usuario.Identificacion),
        //                sessionStorage.setItem('correo', usuario.Correo)
        //            switch (usuario.TipoUsuario) {
        //                case 'Admin':
        //                    window.location.href = "vCategoria";
        //                    break;
        //                case 'Fisico':
        //                    break;
        //                case 'Juridico':
        //                    break;
        //                case 'Cliente':
        //                    break;
        //            }


        //            break;
        //        default:
        //            alert('Ha ocurrido un error')
        //            break;
        //    }



        //}, errorFunction = () => {
        //    this.correo.classList.add("is-invalid");
        //    this.contrasenna.classList.add("is-invalid");

        //});
    }



}