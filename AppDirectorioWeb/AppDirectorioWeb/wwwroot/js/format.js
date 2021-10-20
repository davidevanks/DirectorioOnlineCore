if (typeof $regexp === "undefined") { throw new "El módulo Regexp.js no ha sido importado."; }

/**
 * @description
 * Contiene funciones para formateo de cadenas.
 * Global scope: <strong>$formater</strong>
 * @author Consulting Group Corporation © 2019 - Josué Mercadillo
 * @version 1.0.2
 */
function Formater() {

    /**
     * Formatea el valor de entrada a formato de entero con punto de miles. (Con máscara).
     * @param {String} text Texto con formato o sin formato.
     * @returns {String} Texto formateado a entero p. ej. 1024 => 1.024, 1,024 => 1024
     */
    this.formatIntegerString = function (text) {
        text = (text || "").trim();
        var isNegative = /^[\-]/.test(text); // Obteniendo si la entrada es negativa
        var textPreFormat = text.length > 1 ? text.replace(/[^0-9]/g, "").replace(/^0+/, "") : text.replace(/[^0-9]/g, ""); // Obteniendo solo números enteros
        return (isNegative ? "-" : "") + textPreFormat.replace(/(?!\b)(\d{3}(?=(\d{3})*\b))/g, $params.uiThousandSeparator + "$1"); // Agregando punto de miles;
    };

    /**
     * Formatea el valor de entrada a formato de entero sin puntos de miles. (Sin máscara).
     * @param {String} text Texto con formato o sin formato
     * @returns {String} Texto formateado a entero p. ej. 1024 ; -2024
     */
    this.unformatIntegerString = function (text) {
        text = (text || "").trim();
        var isNegative = /^[\-]/.test(text); // Obteniendo si la entrada es negativa
        var textPreFormat = text.length > 1 ? text.replace(/[^0-9]/g, "").replace(/^0+/, "") : text.replace(/[^0-9]/g, ""); // Obteniendo solo números enteros
        return (isNegative ? "-" : "") + textPreFormat;
    };

    /**
     * Formatea el valor de entrada a formato de decimal nativo. (Con máscara).
     * @param {String} text Texto con formato o sin formato.
     * @returns {String} Texto formateado a decimal p. ej. 1024 => 1.024, 1,024 => 1,02
     */
    this.formatDecimalString = function (text) {
        text = (text || "").trim();
        // Identificación de formato decimal c# 2024.20
        if (text.length - (text.indexOf($params.csDecimalSeparator) + 1) <= 2) {
            text = text.replace($params.csDecimalSeparator, $params.uiDecimalSeparator);
        }
        var indexDecimal = text.lastIndexOf($params.uiDecimalSeparator);
        var textDecimal = "";
        if (indexDecimal > -1) {
            textDecimal = $params.uiDecimalSeparator + text.substring(indexDecimal, indexDecimal + 3).replaceAll($params.uiDecimalSeparator, "");
            text = text.substring(0, indexDecimal);
        }
        var textNumber = $formater.formatIntegerString(text);
        if (textNumber.length === 0 && textDecimal.length > 0) { // Si tiene parte decimal pero no parte entera:
            textNumber = "0";
        }
        return textNumber + textDecimal; // Agregando parte entera formateada y parte decimal.
    };

    /**
     * Formatea el valor de entrada a formato de decimal con coma de decimal o especificado. (Sin máscara).
     * @param {String} text Texto con formato o sin formato.
     * @param {String} decimalSeparator Separador de decimales.
     * @returns {String} Texto formato decimal p. ej. 1024.02 -2024.02
     */
    this.unformatDecimalString = function (text, decimalSeparator) {
        decimalSeparator = decimalSeparator || $params.csDecimalSeparator;
        text = (text || "").trim();
        var isNegative = /^[\-]/g.test(text); // Obteniendo si la entrada es negativa
        var indexDecimal = text.indexOf($params.uiDecimalSeparator);
        var textDecimal = "";
        if (indexDecimal > -1) {
            textDecimal = text.substring(indexDecimal, indexDecimal + 3);
            text = text.substring(0, indexDecimal);
        }
        var textPreFormat = text.length > 1 ? text.replace(/[^0-9]/g, "").replace(/^0+/, "") : text.replace(/[^0-9]/g, ""); // Obteniendo solo números enteros
        var txtInteger = textPreFormat.replace(/[^0-9]+$/g, "");
        if (txtInteger.length === 0 && textDecimal.length > 0) { // Si tiene parte decimal pero no parte entera:
            txtInteger = "0";
        }
        return (isNegative ? "-" : "") + txtInteger + textDecimal.replace($params.uiDecimalSeparator, decimalSeparator); // Agregando simbolo negativo, parte entera sin formato y punto decimal
    };

    /**
     * Obtiene Date desde un texto utilizando moment.
     * @param {String} text Texto con formato.
     * @param {String} format Texto del formato de la cadena.
     * @returns {Date} Fecha.
     */
    this.dateFromString = function (text, format) {
        format = format || $regexp.DateFormatMMDDYYYY;
        return moment(text, format).toDate();
    };

    /**
     * Da formato a una fecha proporcionada.
     * @param {String|Date} date Fecha a formatear {String|Date|Moment}.
     * @param {string} toFormat Formato a utilizar.
     * @param {string} dateFormat Formato actual del objeto date proporcionado.
     * @returns {String} Fecha formateada.
     */
    this.formatDateTimeToString = function (date, toFormat, dateFormat) {
        if (moment.isMoment(date)) {
            return moment(date).format(toFormat);
        } else if (typeof date === "string") {
            return moment(date, dateFormat).format(toFormat);
        } else {
            return moment(date).format(toFormat);
        }
    };

    /**
     * Retorna formato de una entrada.
     * @param {Element} element Entrada a formatear.
     * @param {"mask"|"unmask"} type Identifica si se aplicará formato de máscara o quita el formato.
     * @returns {String} Cadena formateada.
     */
    this.formatEntry = function (element, type) {
        var value = $(element).val(),
            masktype = $attr.dataMasktype(element);
        switch (masktype) {
            case "integer":
                if (type === "mask") {
                    value = $formater.formatIntegerString(value);
                } else {
                    value = $formater.unformatIntegerString(value);
                }
                break;
            case "decimal":
                if (type === "mask") {
                    value = $formater.formatDecimalString(value);
                } else {
                    value = $formater.unformatDecimalString(value, $params.csDecimalSeparator);
                }
                break;
            case "date":
                if (value) {
                    value = $formater.formatDateTimeToString(value, $regexp.DateFormatDDMMYYYY, $regexp.DateFormatDDMMYYYY);
                }
                break;
            case "timespan":
                if (value) {
                    if (type === "mask") {
                        value = $formater.formatDateTimeToString(value, $regexp.DateFormathhmmA, $regexp.DateFormatHHmmss);
                    } else {
                        value = $formater.formatDateTimeToString(value, $regexp.DateFormatHHmmss, $regexp.DateFormathhmmA);
                    }
                }
                break;
            case "datetime":
                if (value) {
                    if (type === "mask") {
                        value = $formater.formatDateTimeToString(value, $regexp.DateFormatDDMMYYYY_hhmmA, $regexp.DateFormatDDMMYYYY_HHmmss);
                    } else {
                        value = $formater.formatDateTimeToString(value, $regexp.DateFormatDDMMYYYY_HHmmss, $regexp.DateFormatDDMMYYYY_hhmmA);
                    }
                }
                break;
            default: // Sin máscara
                break;
        }
        return value;
    };
}

/**
 * Funciones globales para formatos de cadenas y objetos.
 */
var $formater = new Formater();