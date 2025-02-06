export default function Home() {
  return (
    <div className="min-h-screen bg-gray-50">
      {/* Services Section */}
      <section className="container mx-auto px-4 py-16">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          {/* Custom Software Card */}
          <div className="bg-white p-6 rounded-lg shadow-lg">
            <h2 className="text-2xl font-bold text-gray-800 mb-4">
              Custom Software Solutions
            </h2>
            <p className="text-gray-600">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do
              eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
              enim ad minim veniam.
            </p>
          </div>

          {/* Web Development Card */}
          <div className="bg-white p-6 rounded-lg shadow-lg">
            <h2 className="text-2xl font-bold text-gray-800 mb-4">
              Web Development
            </h2>
            <p className="text-gray-600">
              Duis aute irure dolor in reprehenderit in voluptate velit esse
              cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat
              cupidatat.
            </p>
          </div>

          {/* Digital Transformation Card */}
          <div className="bg-white p-6 rounded-lg shadow-lg">
            <h2 className="text-2xl font-bold text-gray-800 mb-4">
              Digital Transformation
            </h2>
            <p className="text-gray-600">
              Sed ut perspiciatis unde omnis iste natus error sit voluptatem
              accusantium doloremque laudantium, totam rem aperiam, eaque ipsa
              quae.
            </p>
          </div>
        </div>
      </section>

      {/* Call to Action Section */}
      {/* <section className="container mx-auto px-4 py-12 text-center">
        <h2 className="text-3xl font-bold text-gray-800 mb-6">
          Ready to Transform Your Business?
        </h2>
        <button
          onClick={handleCallToAction}
          disabled={isLoading}
          className="bg-blue-600 text-white px-8 py-3 rounded-lg font-semibold
                     hover:bg-blue-700 transition-colors disabled:bg-blue-400"
        >
          {isLoading ? "Connecting..." : "Get Started"}
        </button>
        {apiMessage && <p className="mt-4 text-gray-600">{apiMessage}</p>}
      </section> */}

      {/* Testimonials Section */}
      <section className="bg-gray-100 py-16">
        <div className="container mx-auto px-4">
          <h2 className="text-3xl font-bold text-center text-gray-800 mb-12">
            Customer Testimonials
          </h2>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
            {/* Testimonial 1 */}
            <div className="bg-white p-6 rounded-lg shadow">
              <p className="text-gray-600 italic mb-4">
                "Dusyk Dev transformed our business processes with their custom
                software solution. Their expertise and professionalism exceeded
                our expectations."
              </p>
              <p className="font-bold text-gray-800">
                - John Smith, CEO of TechCorp
              </p>
            </div>

            {/* Testimonial 2 */}
            <div className="bg-white p-6 rounded-lg shadow">
              <p className="text-gray-600 italic mb-4">
                "Working with Dusyk Dev was a game-changer for our company.
                Their web development team delivered a beautiful, functional
                website on time and on budget."
              </p>
              <p className="font-bold text-gray-800">
                - Sarah Johnson, Marketing Director
              </p>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}
