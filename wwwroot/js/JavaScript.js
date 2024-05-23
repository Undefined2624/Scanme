$(document).ready(function () {
    $('#duenoModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Botón que activó el modal
        var action = button.data('action'); // Extraer información de los datos del botón
        var modal = $(this);

        if (action === 'create') {
            modal.find('.modal-title').text('Agregar dueño alternativo');
            // Cargar la vista parcial mediante AJAX
            $.ajax({
                url: '/DuenoAlternativo/Create',
                type: 'GET',
                success: function (result) {
                    modal.find('.modal-body').html(result);
                }
            });
        } else if (action === 'edit') {
            modal.find('.modal-title').text('Modificar dueño alternativo');
            // Aquí puedes cargar la vista de edición de manera similar
        }
    });
});
