export interface Branch {
  id: number;
  companyId: number;
  companyName?: string;
  code: string;
  arabicName: string;
  englishName: string;
  arabicAddress: string;
  englishAddress: string;
  phone: string;
  logo: string;
  isActive: boolean;
  createdAt?: string;
  updatedAt?: string;
}

export interface CreateBranchDto {
  companyId: number;
  code: string;
  arabicName: string;
  englishName: string;
  arabicAddress?: string;
  englishAddress?: string;
  phone?: string;
  logo?: string;
}

export interface UpdateBranchDto extends CreateBranchDto {
  isActive?: boolean;
}

