import { useNavigation } from "react-router";
import { Loader } from "../../components/Loader";
import { apiUrl } from "../../config";
import { Resume } from "./types";

// eslint-disable-next-line react-refresh/only-export-components
export async function clientLoader({ params }: { params: { slug: string } }) {
  console.log(apiUrl);
  const response = await fetch(
    `${apiUrl}/resume/get-resume-by-slug/${params.slug}`
  );
  const data: Resume = await response.json();
  await new Promise((resolve) => setTimeout(resolve, 1000));
  return {
    resume: data,
  };
}
clientLoader.hydrate = true as const;

export function HydrateFallback() {
  return <Loader />;
}

export default function Component({
  loaderData,
}: {
  loaderData: { resume: Resume };
}) {
  const { resume } = loaderData;
  const navigation = useNavigation();

  return (
    <>
      {navigation.state === "loading" ? (
        <Loader />
      ) : (
        <div className="max-w-4xl mx-auto p-8 bg-white shadow-lg my-8">
          {/* Header Section */}
          <div className="border-b pb-6 mb-6">
            <h1 className="text-3xl font-bold mb-2">{resume.name}</h1>
            <div className="text-gray-600">
              <p>{resume.email}</p>
              <p>{resume.phone}</p>
            </div>
          </div>

          {/* Summary Section */}
          <div className="mb-8">
            <h2 className="text-2xl font-semibold mb-3">Summary</h2>
            <p className="text-gray-700">{resume.data.summary}</p>
          </div>

          {/* Experience Section */}
          <div className="mb-8">
            <h2 className="text-2xl font-semibold mb-4">Experience</h2>
            {resume.data.experience.map((exp, index) => (
              <div key={index} className="mb-6">
                <h3 className="text-xl font-medium">{exp.title}</h3>
                <p className="text-gray-700 font-medium">{exp.company}</p>
                <p className="text-gray-600 text-sm mb-2">
                  {new Date(exp.startDate).toLocaleDateString()} -{" "}
                  {exp.endDate
                    ? new Date(exp.endDate).toLocaleDateString()
                    : "Present"}
                </p>
                <p className="text-gray-700">{exp.description}</p>
              </div>
            ))}
          </div>

          {/* Education Section */}
          <div className="mb-8">
            <h2 className="text-2xl font-semibold mb-4">Education</h2>
            {resume.data.education.map((edu, index) => (
              <div key={index} className="mb-6">
                <h3 className="text-xl font-medium">{edu.school}</h3>
                <p className="text-gray-700 font-medium">{edu.degree}</p>
                <p className="text-gray-600 text-sm mb-2">
                  {new Date(edu.startDate).toLocaleDateString()} -{" "}
                  {edu.endDate
                    ? new Date(edu.endDate).toLocaleDateString()
                    : "Present"}
                </p>
                <p className="text-gray-700">{edu.description}</p>
              </div>
            ))}
          </div>
        </div>
      )}
    </>
  );
}
