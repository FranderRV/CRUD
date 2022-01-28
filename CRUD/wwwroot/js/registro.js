$(document).ready(function () {
    tipoIdentificacion()

    if (Number($('#idUsuario').val()) > 0) {
        $("#clave").removeAttr("required");
    }
    $("#btnresgistro").click(function () {
        if (validaciones()) {
            $("#formulario").submit();
        }
    });

    $('#btnregistro').click(function (e) {
        e.preventDefault();
        agregarUsuario()
    });

    $('#tipoIdentificacion').change(function (e) {
        e.preventDefault();
        tipoIdentificacion();
    });
    $('#btAgregarNumero').click(function (e) {
        agregarTelefono()
    });

});

function validaciones() {
    if ($("#habilidades").val().length >= 3) {
       
        var str = $("#clave").val()

        if (str.length == 0) {
            return true
        }

        var patt = new RegExp(/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/g);
        var res = patt.test(str);
        
        if (res) {
            return true
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Validación',
                text: 'La contraseña debe poseer: Mínimo 6 caracteres, letras y números',
                footer: ''
            })
            return false
        }
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Validación',
            text: 'Debe añadir más de 3 habilidades blandas',
            footer: ''
        })
        return false
    }

    
}
function agregarUsuario() {
    let form = $('form').serializeArray();
    console.log(form);

}

function tipoIdentificacion() {
    if ($('#tipoIdentificacion').val() == 'N') {
        $('#cedula').mask("0-0000-0000")
    } else {
        $('#cedula').unmask()
    }
}

function agregarTelefono() {

    let numero = $('#nombrecompleto').val()

    $(".listatelefonos").append(`
        <input id="nombrecompleto" class="form-control pb-3" type="text" name="telefonos" maxlength="8" >
        <hr />
`);

}