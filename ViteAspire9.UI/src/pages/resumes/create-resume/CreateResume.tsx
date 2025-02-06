import { Form, useNavigate } from "react-router";
import {
  CreateResumeInputModel,
  EducationInputModel,
  ExperienceInputModel,
} from "./types";
import { useState } from "react";
import { apiUrl } from "../../../config";

export default function CreateResume() {
  const [experiences, setExperiences] = useState<ExperienceInputModel[]>([]);
  const [educations, setEducations] = useState<EducationInputModel[]>([]);
  const [submitting, setSubmitting] = useState(false);
  const navigate = useNavigate();

  const addExperience = () => {
    setExperiences([
      ...experiences,
      {
        title: "",
        company: "",
        startDate: new Date(),
        endDate: null,
        description: "",
      },
    ]);
  };

  const addEducation = () => {
    setEducations([
      ...educations,
      {
        school: "",
        degree: "",
        startDate: new Date(),
        endDate: null,
        description: "",
      },
    ]);
  };

  const updateExperience = (
    index: number,
    field: keyof ExperienceInputModel,
    value: unknown
  ) => {
    const updated = [...experiences];

    // Add date validation for startDate and endDate
    if (field === "startDate" || field === "endDate") {
      const dateValue = value ? new Date(value as string) : null;
      if (dateValue && !isNaN(dateValue.getTime())) {
        updated[index] = { ...updated[index], [field]: dateValue };
      }
    } else {
      updated[index] = { ...updated[index], [field]: value };
    }

    setExperiences(updated);
  };

  const updateEducation = (
    index: number,
    field: keyof EducationInputModel,
    value: unknown
  ) => {
    const updated = [...educations];

    // Add date validation for startDate and endDate
    if (field === "startDate" || field === "endDate") {
      const dateValue = value ? new Date(value as string) : null;
      if (dateValue && !isNaN(dateValue.getTime())) {
        updated[index] = { ...updated[index], [field]: dateValue };
      }
    } else {
      updated[index] = { ...updated[index], [field]: value };
    }

    setEducations(updated);
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setSubmitting(true);
    const formData = new FormData(e.currentTarget);

    const inputModel: CreateResumeInputModel = {
      name: formData.get("name") as string,
      email: formData.get("email") as string,
      phone: formData.get("phone") as string,
      summary: formData.get("summary") as string,
      workExperiences: experiences,
      educations: educations,
    };

    fetch(`${apiUrl}/resume/create-resume`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      body: JSON.stringify(inputModel),
    })
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => {
            navigate(`/resumes/${data.slug}`);
          });
        }
      })
      .finally(() => {
        setSubmitting(false);
      });
  };

  return (
    <div className="max-w-4xl mx-auto p-6">
      <h1 className="text-3xl font-bold mb-8">Create Resume</h1>

      <Form onSubmit={handleSubmit} className="space-y-6">
        {/* Basic Information */}
        <div className="space-y-4">
          <div>
            <label
              htmlFor="name"
              className="block text-sm font-medium text-gray-700"
            >
              Name
            </label>
            <input
              type="text"
              name="name"
              id="name"
              required
              className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
            />
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label
                htmlFor="email"
                className="block text-sm font-medium text-gray-700"
              >
                Email
              </label>
              <input
                type="email"
                name="email"
                id="email"
                required
                className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
              />
            </div>
            <div>
              <label
                htmlFor="phone"
                className="block text-sm font-medium text-gray-700"
              >
                Phone
              </label>
              <input
                type="tel"
                name="phone"
                id="phone"
                required
                className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
              />
            </div>
          </div>

          <div>
            <label
              htmlFor="summary"
              className="block text-sm font-medium text-gray-700"
            >
              Summary
            </label>
            <textarea
              name="summary"
              id="summary"
              rows={4}
              required
              className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
            />
          </div>
        </div>

        {/* Experience Section */}
        <div>
          <div className="flex justify-between items-center mb-4">
            <h2 className="text-xl font-semibold">Work Experience</h2>
            <button
              type="button"
              onClick={addExperience}
              className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
            >
              Add Experience
            </button>
          </div>

          {experiences.map((exp, index) => (
            <div key={index} className="mb-6 p-4 border rounded-lg">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
                <input
                  type="text"
                  value={exp.title}
                  onChange={(e) =>
                    updateExperience(index, "title", e.target.value)
                  }
                  placeholder="Job Title"
                  className="rounded-md border border-gray-300 px-3 py-2"
                />
                <input
                  type="text"
                  value={exp.company}
                  onChange={(e) =>
                    updateExperience(index, "company", e.target.value)
                  }
                  placeholder="Company"
                  className="rounded-md border border-gray-300 px-3 py-2"
                />
              </div>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
                <input
                  type="date"
                  value={exp.startDate.toISOString().split("T")[0]}
                  onChange={(e) =>
                    updateExperience(
                      index,
                      "startDate",
                      new Date(e.target.value)
                    )
                  }
                  className="rounded-md border border-gray-300 px-3 py-2"
                />
                <input
                  type="date"
                  value={exp.endDate?.toISOString().split("T")[0] || ""}
                  onChange={(e) =>
                    updateExperience(
                      index,
                      "endDate",
                      e.target.value ? new Date(e.target.value) : null
                    )
                  }
                  className="rounded-md border border-gray-300 px-3 py-2"
                />
              </div>
              <textarea
                value={exp.description}
                onChange={(e) =>
                  updateExperience(index, "description", e.target.value)
                }
                placeholder="Description"
                rows={3}
                className="w-full rounded-md border border-gray-300 px-3 py-2"
              />
            </div>
          ))}
        </div>

        {/* Education Section */}
        <div>
          <div className="flex justify-between items-center mb-4">
            <h2 className="text-xl font-semibold">Education</h2>
            <button
              type="button"
              onClick={addEducation}
              className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
            >
              Add Education
            </button>
          </div>

          {educations.map((edu, index) => (
            <div key={index} className="mb-6 p-4 border rounded-lg">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
                <input
                  type="text"
                  value={edu.school}
                  onChange={(e) =>
                    updateEducation(index, "school", e.target.value)
                  }
                  placeholder="School"
                  className="rounded-md border border-gray-300 px-3 py-2"
                />
                <input
                  type="text"
                  value={edu.degree}
                  onChange={(e) =>
                    updateEducation(index, "degree", e.target.value)
                  }
                  placeholder="Degree"
                  className="rounded-md border border-gray-300 px-3 py-2"
                />
              </div>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
                <input
                  type="date"
                  value={edu.startDate.toISOString().split("T")[0]}
                  onChange={(e) =>
                    updateEducation(
                      index,
                      "startDate",
                      new Date(e.target.value)
                    )
                  }
                  className="rounded-md border border-gray-300 px-3 py-2"
                />
                <input
                  type="date"
                  value={edu.endDate?.toISOString().split("T")[0] || ""}
                  onChange={(e) =>
                    updateEducation(
                      index,
                      "endDate",
                      e.target.value ? new Date(e.target.value) : null
                    )
                  }
                  className="rounded-md border border-gray-300 px-3 py-2"
                />
              </div>
              <textarea
                value={edu.description}
                onChange={(e) =>
                  updateEducation(index, "description", e.target.value)
                }
                placeholder="Description"
                rows={3}
                className="w-full rounded-md border border-gray-300 px-3 py-2"
              />
            </div>
          ))}
        </div>

        <div className="flex justify-end">
          <button
            type="submit"
            className="px-6 py-3 bg-blue-600 text-white rounded-md hover:bg-blue-700 
                     disabled:bg-blue-400"
            disabled={submitting}
          >
            {submitting ? "Creating..." : "Create Resume"}
          </button>
        </div>
      </Form>
    </div>
  );
}
