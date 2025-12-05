window.guardar = function (){ 

    Swal.fire({
        title: "Bien Hecho!",
        text: "Se ha guardado con exito",
        icon: "success"
    });
}

window.eliminar = function () {
    return Swal.fire({
        title: "Estas segura?",
        text: "No puedes revertir esto!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, Eliminar!"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: "Eliminado!",
                text: "El producto fue eliminado.",
                icon: "success"
            });
        }
        return result.isConfirmed;
    });
}

window.alerta = function () {
    Swal.fire({
        title: "Alerta",
        text: "El producto no se sumo por falta de stock!",
        icon: "info"
    });
}