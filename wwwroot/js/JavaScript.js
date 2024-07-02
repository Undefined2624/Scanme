
function previewFile() {
    const preview = document.getElementById('imagePreview');
    const file = document.getElementById('formFileSm').files[0];
    const reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }

    if (file) {
        reader.readAsDataURL(file); // Lee el archivo y lo envía a onloadend
    } else {
        preview.src = "";
        preview.style.display = 'none'; // Oculta la imagen si no hay archivo
    }
}


$(document).ready(function () {
    debugger;   
    $('body').on('click', '#CreateDuenoAlternativo', function () {
        debugger;
        var MascotaId = $(this).data('mascota-id');
        $('#exampleModal').modal('show');

        // obtenemos la vista parcial y la ponemos dentro del modal
        $.get(CrearDuenoAlternativo, { MascotaId: MascotaId }, function (data) {
            $('#contenidoModal').html(data);
        });
    });

    $('body').on('click', '#EditDuenoAlternativo', function () {
        debugger;
        var DuenoId = $(this).data('dueno-id');
        $('#exampleModal').modal('show');

        // obtenemos la vista parcial y la ponemos dentro del modal
        $.get(EditarDuenoAlternativo, { DuenoId: DuenoId }, function (data) {
            $('#contenidoModal').html(data);
        });
    });

    $('body').on('click', '#AdditionalInfo', function () {
        var MascotaId = $(this).data('mascota-id');
        $('.modal-dialog').addClass('modal-lg');

        $('#exampleModal').modal('show');

        // Obtenemos la vista parcial y la ponemos dentro del modal
        $.get(InformacionAdicional, { mascotaId: MascotaId }, function (data) {
            $('#contenidoModal').html(data);
        });
    });
    debugger;
    $('body').on('click', '#EliminarMascota', function () {
        var MascotaId = $(this).data('mascota-id');        
        $('#exampleModal').modal('show');

        // Obtenemos la vista parcial y la ponemos dentro del modal
        $.get(EliminarMascota, { MascotaId: MascotaId }, function (data) {
            $('#contenidoModal').html(data);
        });
    });

    $('body').on('click', '#EliminarDuenoAlternativo', function () {
        debugger;
        var DuenoId = $(this).data('dueno-id');
        $('#exampleModal').modal('show');

        // Obtenemos la vista parcial y la ponemos dentro del modal
        $.get(EliminarDuenoAlternativo, { DuenoId: DuenoId }, function (data) {
            $('#contenidoModal').html(data);
        });
    });

});
