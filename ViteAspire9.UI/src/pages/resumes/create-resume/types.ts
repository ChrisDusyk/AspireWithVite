export type ExperienceInputModel = {
  title: string;
  company: string;
  startDate: Date;
  endDate: Date | null;
  description: string;
};

export type EducationInputModel = {
  degree: string;
  description: string;
  startDate: Date;
  endDate: Date | null;
  school: string;
};

export type CreateResumeInputModel = {
  name: string;
  email: string;
  phone: string;
  summary: string;
  workExperiences: ExperienceInputModel[];
  educations: EducationInputModel[];
};
