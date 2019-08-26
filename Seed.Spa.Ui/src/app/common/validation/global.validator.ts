import { retry } from "rxjs/operators";

export class GlobalValidator {

  static mailFormat(control: any): ValidationResult {

    var valor = control.value || "";

    var EMAIL_REGEXP = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (valor != "" && (valor.length <= 5 || !EMAIL_REGEXP.test(valor))) {

      return { "incorrectMailFormat": true };
    }

    return null;
  }

  static cpfCnpjIsValid(control: any): ValidationResult {

    var value = control.value || "";

    if (value.length == 14)
      return GlobalValidator.cpfIsValid(control);
    else (value.length == 18)
      return GlobalValidator.cnpjIsValid(control);

  }

  static cpfIsValid(control: any): ValidationResult {
    var soma : number = 0;
    var resto: number;
    var error = { "incorrectCPF": true };

    var cpf = control.value || "";
    cpf = cpf.replace(/[^\d]+/g, '');
    if (cpf.length != 11)
      return error;

    if (cpf == "00000000000" ||
    cpf == "11111111111" ||
    cpf == "22222222222" ||
    cpf == "33333333333" ||
    cpf == "44444444444" ||
    cpf == "55555555555" ||
    cpf == "66666666666" ||
    cpf == "77777777777" ||
    cpf == "88888888888" ||
    cpf == "99999999999")
      return error;
  
  
    for (let i = 1; i <= 9; i++) soma = soma + parseInt(cpf.substring(i - 1, i)) * (11 - i);
    resto = (soma * 10) % 11;
    if ((resto == 10) || (resto == 11)) resto = 0;
    if (resto != cpf.substring(9, 10)) return error;
    soma = 0;
    for (let i = 1; i <= 10; i++) soma = soma + parseInt(cpf.substring(i - 1, i)) * (12 - i);
    resto = (soma * 10) % 11;
    if ((resto == 10) || (resto == 11)) resto = 0;
    if (resto != parseInt(cpf.substring(10, 11))) return error;

    return null;
  
  }

  static cnpjIsValid(control: any): ValidationResult {

    var cnpj = control.value || "";
    var error = { "incorrectCNPJ": true };

    cnpj = cnpj.replace(/[^\d]+/g, '');
    if (cnpj == '') return error;
    if (cnpj.length != 14)
      return error;


    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
      cnpj == "11111111111111" ||
      cnpj == "22222222222222" ||
      cnpj == "33333333333333" ||
      cnpj == "44444444444444" ||
      cnpj == "55555555555555" ||
      cnpj == "66666666666666" ||
      cnpj == "77777777777777" ||
      cnpj == "88888888888888" ||
      cnpj == "99999999999999")
      return error;

    // Valida DVs
    let tamanho : number = cnpj.length - 2;
    let numeros: string = cnpj.substring(0, tamanho);
    let digitos: string = cnpj.substring(tamanho);
    let soma = 0;
    let pos = tamanho - 7;
    for (let i = tamanho; i >= 1; i--) {
      soma += parseInt(numeros.charAt(tamanho - i)) * pos--;
      if (pos < 2)
        pos = 9;
    }
    let resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != parseInt(digitos.charAt(0)))
      return error;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (let i = tamanho; i >= 1; i--) {
      soma += parseInt(numeros.charAt(tamanho - i)) * pos--;
      if (pos < 2)
        pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != parseInt(digitos.charAt(1)))
      return error;

    return null;

  }

}

interface ValidationResult {
  [key: string]: boolean;
}
