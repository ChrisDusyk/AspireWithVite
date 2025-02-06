import {
  Link,
  Links,
  Meta,
  Outlet,
  Scripts,
  ScrollRestoration,
  useNavigation,
} from "react-router";
import { Loader } from "./components/Loader";

export function Layout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <head>
        <meta charSet="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Dusyk Dev</title>
        <Meta />
        <Links />
      </head>
      <body>
        {children}
        <ScrollRestoration />
        <Scripts />
      </body>
    </html>
  );
}

export default function Root() {
  const navigation = useNavigation();
  return (
    <>
      {/* Header Banner */}
      <header className="bg-blue-600 py-6">
        <div className="container mx-auto px-4">
          <div className="flex items-end gap-8">
            <h1 className="text-4xl font-bold text-white">Dusyk Dev</h1>
            <nav>
              <Link
                to="/resumes"
                className="text-white hover:text-blue-200 font-medium px-4 py-2 rounded-lg hover:bg-blue-700 transition-colors"
              >
                Resumes
              </Link>
            </nav>
          </div>
        </div>
      </header>
      {navigation.state === "loading" ? <Loader /> : <Outlet />}
    </>
  );
}
