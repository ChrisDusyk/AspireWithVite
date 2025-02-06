import { type RouteConfig, route } from "@react-router/dev/routes";

export default [
  route("/", "pages/Home.tsx"),
  route("/resumes/", "pages/resumes/ResumeLookup.tsx"),
  route("/resumes/:slug", "pages/resumes/ResumeView.tsx"),
  route("/resumes/create", "pages/resumes/create-resume/CreateResume.tsx"),
  // * matches all URLs, the ? makes it optional so it will match / as well
  route("*?", "Catchall.tsx"),
] satisfies RouteConfig;
