export function Loader() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50">
      <div className="text-center">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-gray-900 mx-auto mb-4"></div>
        <h2 className="text-xl font-semibold text-gray-700">Loading...</h2>
        <p className="text-gray-500 mt-2">
          Please wait while we fetch your data
        </p>
      </div>
    </div>
  );
}
