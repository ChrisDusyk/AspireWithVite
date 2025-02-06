import { useState } from "react";
import { useNavigate } from "react-router";
import { apiUrl } from "../../config";
import { Resume } from "./types";

async function getResumeBySlug(slug: string): Promise<Resume | null> {
  const response = await fetch(`${apiUrl}/resume/get-resume-by-slug/${slug}`);
  if (response.ok) {
    const data = await response.json();
    return data;
  }
  return null;
}

export default function Component() {
  const [resumeSlug, setResumeSlug] = useState("");
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  async function handleSearch() {
    setError(null);
    const resume = await getResumeBySlug(resumeSlug);
    if (resume) {
      await navigate(`/resumes/${resume.slug}`);
    } else {
      setError("Resume not found");
    }
  }

  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    handleSearch();
  }

  return (
    <div className="min-h-screen flex items-center justify-center">
      <div className="container max-w-md mx-auto p-8 bg-white shadow-lg rounded-lg">
        <div className="flex justify-between items-center mb-6">
          <h1 className="text-3xl font-bold">Resume Lookup</h1>
          <button
            onClick={() => navigate("/resumes/create")}
            className="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-md transition-colors"
          >
            Create New
          </button>
        </div>
        <p className="text-gray-600 mb-4">
          Enter a resume slug to view a resume.
        </p>
        <form onSubmit={handleSubmit} className="space-y-4">
          <input
            type="text"
            placeholder="Resume Slug"
            value={resumeSlug}
            onChange={(e) => setResumeSlug(e.target.value)}
            className="w-full border border-gray-300 rounded-md p-2"
          />
          {error && <p className="text-red-500">{error}</p>}
          <button
            type="submit"
            className="w-full bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-md transition-colors"
          >
            Search
          </button>
        </form>
      </div>
    </div>
  );
}
