if (typeof $alert === "undefined") { throw new "El módulo alerts.js no ha sido importado."; }
if (typeof $formater === "undefined") { throw new "El módulo format.js no ha sido importado."; }

const $save = 0, $edit = 1, $delete = 2, $download = 3, $send = 4; $aprobar = 5;// Etc...

/**
 * @description
 * Contiene funciones para envío de formularios.
 * Global scope: <strong>$form</strong>
 * @author Consulting Group Corporation © 2019 - Josué Mercadillo
 * @version 1.0.4
 */
function Form() {

    /**
    * Evento que se ejecuta al realizar submit al formulario indicado.
    * @param {Event} event OObjeto de evento.
    * @param {Number} type Tipo de mensaje que de desea mostrar.
    * @param {ConfirmCallback} callback Función a ejecutarse durante la confirmación del formulario.
    * @example
    * 
    * HTML:
    * 
    * <form asp-controller="Home" asp-action="ValidationsDa" id="formid" onsubmit="$form.onSubmit(event, $save, _confirm);" method="post" class ="card card-body">
    * ...
    * </form>
    * 
    * Javascript:
    * 
    * var _confirm = function (form, result) {
    *     // Confirmación aceptada.
    *     if (result) {
    *         // Creando petición async.
    *         $form.xhrSubmit({
    *             form: form,
    *             success: function (xhr) {
    *                 alert(":3");
    *             },
    *             error: function (xhr) {
    *                 alert(":C");
    *             },
    *             change: undefined,  // new XHRCallback(xhr);
    *             formData: undefined // new FormData(form);
    *         });
    *     }
    * };
    * 
    */
    this.onSubmit = function (event, type, callback) {
        event.preventDefault();
        var form = event.target,
            funct = "";
        type = type || $save;

        if ($form.isValid(form)) {
            switch (type) {
                case $save:
                    funct = "confirmSave";
                    break;
                case $edit:
                    funct = "confirmEdit";
                    break;
                case $aprobar:
                    funct = "confirmAprobar";
                    break;
                case $delete:
                    funct = "confirmDelete";
                    break;
                case $download:
                    funct = "confirmDownload";
                    break;
                case $send:
                    funct = "confirmSend";
                    break;
                default:
                    throw "Type: " + type + ". Tipo de submit no contemplado.";
            }

            $alert[funct].call().then(function (result) {
                if (result === true) {
                    callback(form, true);
                } else if (result["value"]) {
                    callback(form, true);
                } else {
                    callback(form, false);
                }
            });
        } else {
            // Form is not valid
        }
    };

    /**
     * Envía el formulario de forma nativa y sin máscaras.
     * @param {Element} form Formulario a envíar.
     */
    this.nativeSubmit = function (form) {
        $form.unformattingMasks(form);
        form.submit();
        $form.formattingMasks(form);
    };

    /**
     * Función para submitir formulario indicado.
     * @param {XHROptions} options Funciones a ejecutarse.
     */
    this.xhrSubmit = function (options) {
        //$alert.processing();

        var opts = new XHROptions();
        $.extend(opts, options);

        var url = window.location.href;
        var xhr = new XMLHttpRequest();
        xhr.open(options.form.method, options.form.action, true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                if (typeof opts.success !== "undefined")
                    opts.success(xhr);
            } else {
                if (xhr.readyState === 4 && xhr.status !== 200) {
                    if (typeof opts.error !== "undefined")
                        opts.error(xhr);
                }
            }

            if (typeof opts.change !== "undefined")
                opts.change(xhr);
        };
        //xhr.onload = function () {
        //    if (xhr.status === 200) {
        //        if (xhr.responseURL !== url) {
        //            window.location.href = xhr.responseURL;
        //        }
        //    }
        //};

        var formData = {};
        if (typeof opts.formData === "undefined") {
            formData = $form.getFormData(opts.form);
        } else {
            if (typeof opts.formData === "function") {
                formData = opts.formData.call();
            } else {
                formData = opts.formData;
            }
        }
        xhr.send(formData);
    };

    /**
     * Verifica si jquery validation es true o falso
     * @param {Element} form formulario que se desea verificar si ha pasado la validación o no
     * @returns {Boolean} Devuelve True o False si la validacíon es correcta o no
     */
    this.isValid = function (form) {
        if (typeof $.validator !== "undefined") {
            return $(form).valid();
        } else {
            return true;
        }
    };

    /**
     * Quita formatos de máscara a entradas del formulario.
     * @param {HTMLFormElement} form Formulario a aplicar.
     */
    this.unformattingMasks = function (form) {
        $(form).find("input[data-val-masktype]").each(function (index, element) {
            var value = $formater.formatEntry(element, "unmask");
            $(element).val(value);
        });
    };

    /**
     * Aplica formatos de máscara a entradas del formulario.
     * @param {HTMLFormElement} form Formulario a aplicar.
     */
    this.formattingMasks = function (form) {
        $(form).find("input[data-val-masktype]").each(function (index, element) {
            var value = $formater.formatEntry(element, "mask");
            $(element).val(value);
            $(element).keyup();
        });
    };

    /**
     * Obtiene FormData deserializado y sin formatos.
     * @param {Element} form Formulario a serializar.
     * @returns {FormData} Objeto serializado.
     */
    this.getFormData = function (form) {

        $form.unformattingMasks(form);

        var formData = new FormData(form);

        $form.formattingMasks(form);

        return formData;
    };

    /**
     * Crea el observable para el formulario.
     * @param {string} formSelector Form element CSS Selector.
     * @param {bool} showRequiredIndicator Determina si mostrara el indicador de campos requeridos.
     * @param {bool} ignore Determina si mostrara el indicador de campos requeridos.
     */
    this.recreateUnobtrosive = function (formSelector, showRequiredIndicator, ignore) {
        showRequiredIndicator = showRequiredIndicator || false;

        var form = $(formSelector)
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin*/
        $.validator.defaults.ignore = ignore ? ".data-val-ignore" : ":hidden, .data-val-ignore";

        $.validator.unobtrusive.parse(form);

        if (showRequiredIndicator) {
            $validations.DataValRequiredIndicatorForm(formSelector);
        }
    };

    /**
     * Retorna el tipo de formulario de acuerdo a un tipo de transacción.
     * @param {string} tipoFormulario TipoTransaccion usada en c#.
     * @returns {int} Tipo de formulario.
     */
    this.typeForm = function (tipoFormulario) {
        var type = -1;

        switch (tipoFormulario) {
            case TipoTransaccion_Insertar:
                type = $save;
                break;
            case TipoTransaccion_Modificar:
                type = $edit;
                break;
            case TipoTransaccion_Eliminar:
                type = $delete;
                break;
            case TipoTransaccion_Duplicar:
                type = $save;
                break;
            default:
                type = -1;
                break;
        }

        return type;
    };
}

/**
 * Funciones globales para envío de formularios.
 */
const $form = new Form();

/**
 * Función a ejecutarse en petición XHR.
 * @param {XMLHttpRequest} xhr Objeto de petición actual.
 */
function XHRCallback(xhr) { }

/**
 * Función a ejecutarse antes de submitir un formulario.
 * @param {Element} form Formulario que se está submitiendo.
 * @param {Boolean} result Resultado de confirmación alerta.
 */
function ConfirmCallback(form, result) { }

/**
 * Opciones para XHR.
 * @param {Element} form Formulario a submitir.
 * @param {XHRCallback} success Función a ejecutar cuando el estado sea 4 y el código de respuesta 200 (OK).
 * @param {XHRCallback} error Función a ejecutar cuando el estado sea 4 y el código de respuesta diferente de 200 (No OK).
 * @param {XHRCallback} change Función a ejecutar en cualquier estado y código de respuesta.
 * @param {FormData|Function} formData (Optional) Objeto a enviar en la petición.
 */
function XHROptions(form, success, error, change, formData) {
    this.form = form;
    this.success = success;
    this.error = error;
    this.change = change;
    this.formData = formData;
}