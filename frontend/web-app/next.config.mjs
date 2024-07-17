/** @type {import('next').NextConfig} */
const nextConfig = {
    images: {
        domains: [
            'cdn.pixabay.com',
            'pixabay.com'
        ]
    },

    output: 'standalone'
};

export default nextConfig;
