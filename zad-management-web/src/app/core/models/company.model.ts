export interface Company {
  id: number;
  code: string;
  arabicName: string;
  englishName: string;
  arabicAddress: string;
  englishAddress: string;
  country: string;
  city: string;
  language: string;
  phone: string;
  website: string;
  logo: string;
  isActive: boolean;
  createdAt?: string;
  updatedAt?: string;
}

export interface CreateCompanyDto {
  code: string;
  arabicName: string;
  englishName: string;
  arabicAddress?: string;
  englishAddress?: string;
  country?: string;
  city?: string;
  language?: string;
  phone?: string;
  website?: string;
  logo?: string;
}

export interface UpdateCompanyDto extends CreateCompanyDto {
  isActive?: boolean;
}

