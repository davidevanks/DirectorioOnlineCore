/**
 * @description
 * Contiene funciones para levantar alertas de forma global.
 * Global scope: <strong>$alert</strong>
 * @author Consulting Group Corporation © 2019 - Josué Mercadillo
 * @version 1.1.2
 */
function Alerts() {

    /**
     * Crea una función de alerta nativo o sweetalert.
     * @param {Object} options Optiones para swal.
     * @returns {Function} Función de alerta nativo o swal.
     */
    this.alert = function (options) {
        hideModals();
        options["allowEnterKey"] = true;

        if (typeof swal === "undefined") {
            if (options["confirmButtonText"]) {
                return confirm(options["title"] + "\n" + options["text"]);
            } else {
                return alert(options["title"] + "\n" + options["text"]);
            }
        } else if (typeof swal.fire !== "undefined") {
            return swal.fire(options);
        } else {
            return swal(options);
        }
    };

    /**
     * Función para mostrar sweetalert de confirmación al guardar.
     * @param {String} text Texto que se desea mostrar en el cuerpo del mensaje.
     * @returns {Promise} Devuelve una promesa.
     */
    this.confirmSave = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Guardar",
                //text: text || "¿Está seguro de que desea guardar los datos?",
                text: text || "¿Está seguro de que desea guardar el registro?",
                type: "warning",
                reverseButtons: true,
                showCloseButton: true,
                showCancelButton: true,
                confirmButtonText: "Aceptar",
                cancelButtonText: "Cancelar"
            }));
            showModals();
        });
    };

    /**
     * Función para mostrar sweetalert de confirmación al modificar.
     * @param {String} text Texto que se desea mostrar en el cuerpo del mensaje.
     * @returns {Promise} Devuelve una promesa.
     */
    this.confirmEdit = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Modificar",
                //text: text || "¿Está seguro de que desea modificar los datos?",
                text: text || "¿Está seguro de que desea modificar el registro?",
                type: "warning",
                reverseButtons: true,
                showCloseButton: true,
                showCancelButton: true,
                confirmButtonText: "Aceptar",
                cancelButtonText: "Cancelar"
            }));
            showModals();
        });
    };

    this.confirmAprobar = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Confirmar",
                //text: text || "¿Está seguro de que desea modificar los datos?",
                text: text || "¿Está seguro de que desea realizar la acción seleccionada?",
                type: "warning",
                reverseButtons: true,
                showCloseButton: true,
                showCancelButton: true,
                confirmButtonText: "Aceptar",
                cancelButtonText: "Cancelar"
            }));
            showModals();
        });
    };

    /**
     * Función para mostrar sweetalert de confirmación al eliminar
     * @param {String} text Texto que se desea mostrar en el cuerpo del mensaje
     * @returns {Promise} Devuelve una promesa
     */
    this.confirmDelete = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Eliminar",
                //text: text || "¿Está seguro de que desea eliminar este elemento?",
                text: text || "¿Está seguro de que desea eliminar el registro?",
                type: "warning",
                reverseButtons: true,
                showCloseButton: true,
                showCancelButton: true,
                confirmButtonText: "Aceptar",
                cancelButtonText: "Cancelar"
            }));
            showModals();
        });
    };
    /**
        * Función para mostrar sweetalert de confirmación al Anular
        * @param {String} text Texto que se desea mostrar en el cuerpo del mensaje
        * @returns {Promise} Devuelve una promesa
        */
    this.confirmRevoke = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                //title: "Anular",
                title: "Modificar estado",
                //text: text || "¿Está seguro de que desea anular este elemento?",
                text: text || "¿Está seguro que desea modificar el estado del registro?",
                type: "warning",
                reverseButtons: true,
                showCloseButton: true,
                showCancelButton: true,
                confirmButtonText: "Aceptar",
                cancelButtonText: "Cancelar"
            }));
            showModals();
        });
    };
    /**
     * Función para mostrar sweetalert de éxito.
     * @param {String} text Texto que se desea mostrar en el cuerpo del mensaje.
     * @returns {Promise} Devuelve una promesa.
     */
    this.success = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Éxito",
                text: text || "El registro ha sido procesado correctamente.",
                type: "success",
                showCloseButton: true,
                confirmButtonText: "Aceptar"
            }));
            showModals();
        });
    };
    this.successSave = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Éxito",
                text: text || "El registro se ha guardado correctamente.",
                type: "success",
                showCloseButton: true,
                confirmButtonText: "Aceptar"
            }));
            showModals();
        });
    };
    this.successEdit = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Éxito",
                text: text || "El registro se ha modificado correctamente.",
                type: "success",
                showCloseButton: true,
                confirmButtonText: "Aceptar"
            }));
            showModals();
        });
    };
    this.successDelete = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Éxito",
                text: text || "El registro se ha eliminado correctamente.",
                type: "success",
                showCloseButton: true,
                confirmButtonText: "Aceptar"
            }));
            showModals();
        });
    };
    this.successRevoke = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Éxito",
                text: text || "El registro ha cambiado de estado correctamente.",
                type: "success",
                showCloseButton: true,
                confirmButtonText: "Aceptar"
            }));
            showModals();
        });
    };

    /**
     * Función para mostrar sweetalert de éxito o error.
     * @param {String} data Data tipo Result.
     * @returns {Promise} Devuelve una promesa.
     */
    this.complete = function (data) {
        return new Promise(function (resolve, reject) {
            if (data.outcome) {
                resolve($alert.success(data.message));
            } else {
                resolve($alert.error(data.message));
            }
            showModals();
        });
    };

    /**
     * Función para mostrar sweetalert para mostrar información.
     * @param {String} text Texto que se desea mostrar en el cuerpo del mensaje.
     * @returns {Promise} Devuelve una promesa.
     */
    this.info = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Ayuda",
                text: text || "",
                type: "info",
                showCloseButton: true,
                confirmButtonText: "Aceptar"
            }));
            showModals();
        });
    };

    /**
     * Función para mostrar sweetalert para mostrar error.
     * @param {String} text Texto que se desea mostrar en el cuerpo del mensaje.
     * @returns {Promise} Devuelve una promesa.
     */
    this.error = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Error",
                text: text || "Lo sentimos, ocurrió un error inesperado.",
                type: "error",
                showCloseButton: true,
                confirmButtonText: "Aceptar"
            }));
            showModals();
        });
    };

    /**
     * Función para mostrar sweetalert para mostrar peligro.
     * @param {String} text Texto que se desea mostrar en el cuerpo del mensaje.
     * @returns {Promise} Devuelve una promesa.
     */
    this.peligro = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Alerta",
                text: text || "Lo sentimos, occurrió un error inesperado.",
                type: "warning",
                showCloseButton: true,
                confirmButtonText: "Aceptar"
            }));
            showModals();
        });
    };

    this.repetido = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Error",
                text: text || "Lo sentimos, occurrió un error inesperado.",
                type: "warning",
                showCloseButton: true,
                confirmButtonText: "Aceptar"
            }));
            showModals();
        });
    };

    this.confirmSend = function (text) {
        return new Promise(function (resolve, reject) {
            resolve($alert.alert({
                title: "Enviar",
                text: text || "¿Está seguro de que desea enviar este mensaje?",
                type: "warning",
                reverseButtons: true,
                showCloseButton: true,
                showCancelButton: true,
                confirmButtonText: "Aceptar",
                cancelButtonText: "Cancelar"
            }));
            showModals();
        });
    };

    this.Mensaje = function (options) {
        return new Promise(function (resolve, reject) {
            $.extend(options, { reverseButtons: true });
            resolve($alert.alert(options));
            showModals();
        });
    };

    /**
     * Oculta los modales activos en la vista para mostrar foco en sweetalert2.
     * Agrega la clase swalert2-hide para identificar los modales ocultos.
     */
    function hideModals() {
        $("div.modal.show").hide().addClass("swalert2-hide");
    }

    /**
     * Muestra los modales ocultos mediante la clase swalert2-hide que fueron oculados, y remueve dicha clase.
     */
    function showModals() {
        $("div.modal.swalert2-hide").show().removeClass("swalert2-hide");
    }
}

/**
 * Funciones globales de generación de alertas.
 */
const $alert = new Alerts();