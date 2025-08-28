import { Component } from '@angular/core';
import { MappingService } from '../Services/mapping.service';

@Component({
  selector: 'app-mapping',
  templateUrl: './mapping.component.html',
  styleUrls: ['./mapping.component.css']
})
export class MappingComponent {
  functoidTypes: string[] = [
    'Equal',
    'LogicalNot',
    'Size',
    'StringLeftOrRight',
    'ValueMapping',
    'StringTrim'
  ];

  formData: any = {
    functoidType: 'Equal'
  };

  inputsString = '';
  parametersString = '';

  
  chainFlowInput = '';

  result: any = null;
  allStepsOutput: any[] = [];

  constructor(private mappingService: MappingService) {}

  onSubmit(): void {
    try {
      const inputs = this.inputsString ? JSON.parse(this.inputsString) : [];
      const parameters = this.parametersString ? JSON.parse(this.parametersString) : {};

      const request = {
        functoidType: this.formData.functoidType,
        inputs: inputs,
        parameters: parameters
      };

      this.mappingService.executeMapping(request).subscribe({
        next: (res) => {
          console.log('✅ Başarılı:', res);
          this.result = res;
        },
        error: (err) => {
          console.error('❌ Hata:', err);
          this.result = err.error?.text || JSON.stringify(err.error) || err.message || 'Bilinmeyen hata';
        }
      });
    } catch (e) {
      this.result = '❌ Geçersiz giriş!';
    }
  }

onChainedSubmit(): void {
  try {
    const flow = JSON.parse(this.chainFlowInput);

    if (!Array.isArray(flow)) {
      this.result = '❌ Giriş bir dizi (array) olmalı!';
      return;
    }

    this.mappingService.executePipeline(flow).subscribe({
      next: (res) => {
        // Backend, sonucu { result: ... } şeklinde sarmalamış olabilir
        this.result = res.result ?? res;
      },
      error: (err) => {
        this.result = `❌ ${err.error?.text || JSON.stringify(err.error) || 'Hata oluştu'}`;
      }
    });
  } catch (e) {
    this.result = '❌ Geçersiz JSON!';
  }
}


  formatResult(val: any): string {
    return typeof val === 'object' ? JSON.stringify(val) : String(val);
  }
}
