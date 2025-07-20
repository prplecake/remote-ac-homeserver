package app

// A Configuration is the main config object
type Configuration struct {
	App Config
	Database DatabaseConfig
	Templates TemplateConfig
	Media MediaConfig
}

// A Config holds app-specific configuration
type Config struct {
	Port string
}

// A DatabaseConfig holds database-specific configuration
type DatabaseConfig struct {
	Username, Password, Hostname, Name, Path string
	Port int
}

// A TemplateConfig holds template-specific configuration
type TemplateConfig struct {
	Path string
}

// A MediaConfig holds media-specific configuration 
type MediaConfig struct {
	Path string
	ThumbnailsPath string
}

