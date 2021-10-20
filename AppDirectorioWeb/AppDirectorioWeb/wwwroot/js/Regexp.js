/**
 * @description
 * Contiene constantes generales de expresiones regulares y formatos.
 * Global scope: <strong>$regexp</strong>
 * @author Consulting Group Corporation © 2019 - Josué Mercadillo
 * @version 1.0.0
 */
function Regexp() {
    /**
     * Regexp: dd/MM/yyyy
     * @example 31/12/2000
     */
    this.DateRegex_Js_DDMMYYYY = /([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}/g;

    /**
     * Regexp: dd/MM/yyyy hh:mm A
     * @example 31/12/2000 12:59 PM
     */
    this.DateRegex_Js_DDMMYYYYhhmmA = /([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}( )([0-2][0-4]:[0-5][0-9])( )((AM)|(PM))/g;

    /**
     * Regexp: MM/dd/yyyy hh:mm A.
     * @example 12/31/2000 12:59 PM
     */
    this.DateRegex_Js_MMDDYYYYhhmmA = /(((0)[0-9])|((1)[0-2]))(\/)([0-2][0-9]|(3)[0-1])(\/)\d{4}( )([0-2][0-4]:[0-5][0-9])( )((AM)|(PM))/g;

    /**
     * Regexp: MM/dd/yyyy
     * @example 12/31/2000
     */
    this.DateRegex_Csharp_MMDDYYYY = /(((0)[0-9])|((1)[0-2]))(\/)([0-2][0-9]|(3)[0-1])(\/)\d{4}/g;

    /**
     * Regexp: MM/dd/yyyy HH:mm:ss
     * @example 12/31/2000 24:59:59
     */
    this.DateRegex_Csharp_MMDDYYYYHHmmss = /(((0)[0-9])|((1)[0-2]))(\/)([0-2][0-9]|(3)[0-1])(\/)\d{4}( )([0-2][0-4]:[0-5][0-9]:[0-5][0-9])/g;

    /**
     * Regexp: hh:mm A
     * @example 12:59 PM
     */
    this.TimeRegex_Js_hhmmA = /[0-2][0-4]:[0-5][0-9] (AM|PM)/g;

    /**
     * Encuentra la coincidencia de caracteres que no se ven dentro de la cadena de texto.
     * Saltos de línea y tabulaciones para ser contadas como caracteres a ingresar a la base.
     * @example Hello\n\r
     *          \tWord!
     */
    this.TextArea_Js_Characters_Invisibles = /(\n\r)+(\t)+/gm;

    /**
     * Validación de numeros decimales o enteros.
     * 9.999.999,99
     * @example
     * 999
     * 9.999
     * 9.999.999,99
     */
    this.NumberRegex_Js = /^[\-]?[0-9]{1,3}(\.[0-9]{3})*(\,[0-9]{1,2})?$/;

    this.NumberRegex_Csharp = /^[\-]?[0-9]*(\.[0-9]{1,2})?$/;

    /**
     * Formato: MM/DD/YYYY
     * @example 12/31/2000
     */
    this.DateFormatMMDDYYYY = "MM/DD/YYYY";

    /**
     * Formato: MM/DD/YYYY hh:mm A
     * @example 12/31/2000 12:59 PM
     */
    this.DateFormatMMDDYYYY_hhmmA = "MM/DD/YYYY hh:mm A";

    /**
     * Formato: MM/DD/YYYY HH:mm:ss
     * @example 12/31/2000 24:59:59
     */
    this.DateFormatMMDDYYYY_HHmmss = "MM/DD/YYYY HH:mm:ss";

    /**
     * Formato: MM/DD/YYYY HH:mm:ss
     * @example 31/12/2000 24:59:59
     */
    this.DateFormatDDMMYYYY_HHmmss = "DD/MM/YYYY HH:mm:ss";

    /**
     * Formato: DD/MM/YYYY
     * @example 31/12/2000
     */
    this.DateFormatDDMMYYYY = "DD/MM/YYYY";

    /**
     * Formato: DD/MM/YYYY hh:mm A
     * @example 31/12/2000 12:59 PM
     */
    this.DateFormatDDMMYYYY_hhmmA = "DD/MM/YYYY hh:mm A";

    /**
     * Formato: hh:mm A
     * @example 12:59 PM
     */
    this.DateFormathhmmA = "hh:mm A";

    /**
     * Formato: HH:mm:ss
     * @example 24:59:59
     */
    this.DateFormatHHmmss = "HH:mm:ss";
}

/**
 * Provee constantes globales de expresiones regulares y formatos.
 */
const $regexp = new Regexp();