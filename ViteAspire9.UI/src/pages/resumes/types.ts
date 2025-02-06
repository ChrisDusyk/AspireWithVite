export type Resume = {
  id: string; // Guid maps to string in TypeScript
  slug: string;
  name: string;
  email: string;
  phone: string;
  data: ResumeData;
};

export type ResumeData = {
  summary: string;
  experience: Experience[];
  education: Education[];
};

export type Education = {
  school: string;
  degree: string;
  startDate: string; // DateTime maps to string in TypeScript
  endDate: string | null; // DateTime? maps to string | null
  description: string;
};

export type Experience = {
  title: string;
  company: string;
  startDate: string;
  endDate: string | null;
  description: string;
};
